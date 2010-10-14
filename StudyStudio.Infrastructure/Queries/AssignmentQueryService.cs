using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using StudyStudio.Domain;
using StudyStudio.Infrastructure.Scripts;

namespace StudyStudio.Infrastructure.Queries
{
    public class AssignmentQueryService : IAssignmentQueryService
    {
        private readonly IDocumentStore _db;

        public AssignmentQueryService(IDocumentStore db)
        {
            _db = db;
        }

        public AssignmentDetails Get(string id)
        {
            List<ExerciseDetails> exercises;
            using (var session = _db.OpenSession())
            {
               var results = session.LuceneQuery<Exercise>(DBUtils.ExercisesByAssignmentIndexName)
                   .Include("ExerciseId")
                   .SelectFields<dynamic>("ExerciseId", "Body")
                   .Select(x=> session.Load<Exercise>(x.ExerciseId))
                   .ToList();

                exercises = results.Select(r => new ExerciseDetails {__document_id = r.ExerciseId, Body = r.Body}).ToList();
            }

            return new AssignmentDetails {Exercises = exercises, Id = id};
        }
    }
}