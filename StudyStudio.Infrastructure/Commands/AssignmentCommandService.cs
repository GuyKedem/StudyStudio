using System.Collections.Generic;
using Raven.Client;
using Raven.Client.Document;
using StudyStudio.Domain;

namespace StudyStudio.Infrastructure.Commands
{
    public class AssignmentCommandService : IAssignmentCommandService
    {
        private readonly IDocumentStore _db;

        public AssignmentCommandService(IDocumentStore db)
        {
            _db = db;
        }

        public void CreateAssignment(IList<string> exerciseIds)
        {
            using (var session = _db.OpenSession())
            {
                var assignment = new Assignment();
                session.Store(assignment);

                // Patch all specified exercises with the new assignment id.
                //session.Advanced.DatabaseCommands.UpdateByIndex();
                session.SaveChanges();
            }
        }
    }
}