using System.Collections.Generic;

namespace StudyStudio.Infrastructure.Commands
{
    public interface IAssignmentCommandService
    {
        void CreateAssignment(IList<string> exerciseIds);
    }
}