using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFramework_exercise.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EmploymentDate { get; set; }

        public virtual ICollection<Employment> Employments { get; set; }
    }
}