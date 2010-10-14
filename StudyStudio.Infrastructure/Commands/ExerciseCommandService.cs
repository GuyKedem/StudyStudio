using Raven.Client;
using StudyStudio.Domain;

namespace StudyStudio.Infrastructure.Commands
{
    public class ExerciseCommandService : IExerciseCommandService
    {
        private readonly IDocumentStore _db;

        public ExerciseCommandService(IDocumentStore db)
        {
            _db = db;
        }

        public void CreateExercise(string body)
        {
            var exercise = new Exercise {Body = body};

            using (var session = _db.OpenSession())
            {
                session.Store(exercise);
                session.SaveChanges();
            }
        }
    }
}
