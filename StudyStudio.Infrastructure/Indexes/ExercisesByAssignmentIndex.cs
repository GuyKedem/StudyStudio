using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Indexes;
using StudyStudio.Domain;

namespace StudyStudio.Infrastructure.Indexes
{
    class ExercisesByAssignmentIndex : AbstractIndexCreationTask<Exercise>
    {
        public ExercisesByAssignmentIndex()
        {
            Map = exercises => from e in exercises
                               from aid in e.Assignments
                               select new {Assignment = aid};
        }
    }
}
