using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyStudio.Infrastructure.Queries
{
    public interface IAssignmentQueryService
    {
        AssignmentDetails Get(string id);
    }
}