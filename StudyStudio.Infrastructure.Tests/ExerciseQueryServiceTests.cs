using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;
using StudyStudio.Domain;
using StudyStudio.Infrastructure.Queries;
using Xunit;

namespace StudyStudio.Infrastructure.Tests
{
    public class ExerciseQueryServiceTests : RavenTestFixture
    {
        [Fact]
        public void CanQueryAllExercises()
        {
            var input = new List<Exercise>
                               {
                                   new Exercise {Body = "This is the body1"},
                                   new Exercise {Body = "This is the body2"}
                               };
            
            using (var db = GetConfiguredDB())
            {
                using (var session = db.OpenSession())
                {
                    session.StoreCollection(input);
                    session.SaveChanges();
                }

                WaitForNonStaleResults(db);

                var expected = new List<ExerciseDetails>
                               {
                                   new ExerciseDetails {Body = input[0].Body, __document_id = input[0].Id},
                                   new ExerciseDetails {Body = input[1].Body, __document_id = input[1].Id}
                               };

                var sut = new ExerciseQueryService(db);
                var result = sut.BrowseExercises();

                Assert.Equal(expected, result);
            }
        }

        //[Fact]
        //public void Can
    }
}
