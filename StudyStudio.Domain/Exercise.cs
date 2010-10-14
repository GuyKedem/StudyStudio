using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyStudio.Domain
{
    public class Exercise
    {
        public string Id { get; set; }
        public string Body { get; set; }

        public IEnumerable<string> Assignments { get; set; }
    }
}