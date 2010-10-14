using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Document;
using StudyStudio.Domain;
using StudyStudio.Infrastructure.Queries;
using Xunit;

namespace StudyStudio.Infrastructure.Tests
{
    public class AssignmentQueryServiceTests : RavenTestFixture
    {
        [Fact]
        public void CanGetAssignmentDetails()
        {
            // Arrange
            var exercises = new List<Exercise>
                                   {
                                       new Exercise{Body = "This is the body1"},
                                       new Exercise{Body = "This is the body2"}
                                   };

            using (var db = GetConfiguredDB())
            {
                IList<string> exercisesIds;
                string assignmentId;
                
                using (var session = db.OpenSession())
                {
                    session.StoreCollection(exercises);
                    exercisesIds = exercises.Select(e => e.Id).ToList();
                    var assignment = new Assignment {ExerciseIds = exercisesIds};
                    session.Store(assignment);
                    session.SaveChanges();
                    assignmentId = assignment.Id;
                }

                WaitForNonStaleResults(db);

                // Act
                var sut = new AssignmentQueryService(db);
                var result = sut.Get(assignmentId);

                // Assert
                Assert.Equal(exercisesIds, result.Exercises.Select(e => e.__document_id));
            }
        }
    }
}
