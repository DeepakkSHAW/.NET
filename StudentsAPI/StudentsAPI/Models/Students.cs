using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsAPI.Models
{
    public class Students
    {
        public int StudentID  { get; set; }
        public string StudentName { get; set; }

        public string StudentContry { get; set; }

        public DateTime StudentDOB { get; set; }

    }
}
