using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;
using StudyStudio.Domain;
using StudyStudio.Infrastructure.Scripts;

namespace StudyStudio.Infrastructure.Queries
{
    public class ExerciseQueryService : IExerciseQueryService
    {
        private readonly IDocumentStore _db;

        public ExerciseQueryService(IDocumentStore db)
        {
            _db = db;
        }

        public IList<ExerciseDetails> BrowseExercises()
        {
            IList<ExerciseDetails> exercises;
            using (var session = _db.OpenSession())
            {
                exercises = session.LuceneQuery<Exercise>(DBUtils.ExercisesByBodyIndexName).
                    SelectFields<ExerciseDetails>("Body", "__document_id").ToList();
            }

            return exercises;
        }
    }
}