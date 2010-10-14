using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyStudio.Domain
{
    public class Assignment
    {
        public string Id { get; set; }
        public IList<string> ExerciseIds { get; set; }
    }
}
