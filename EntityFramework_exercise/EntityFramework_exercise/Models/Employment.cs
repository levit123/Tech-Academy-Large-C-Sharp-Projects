using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFramework_exercise.Models
{
    public enum SecurityLevel
    {
        A, B, C, D
    }

    public class Employment
    {
        public int EmploymentID { get; set; }
        public int ExperimentID { get; set; }
        public int EmployeeID { get; set; }
        public SecurityLevel? SecurityLevel { get; set; }

        public virtual Experiment Experiment { get; set; }
        public virtual Employee Employee { get; set; }
    }
}