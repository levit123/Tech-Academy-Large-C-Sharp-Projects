using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EntityFramework_exercise.Models;

namespace EntityFramework_exercise.DAL
{
    public class BlackMesaInitializer : System.Data.Entity. DropCreateDatabaseIfModelChanges<BlackMesaContext>
    {
        protected override void Seed(BlackMesaContext context)
        {
            var employees = new List<Employee>
            {
                new Employee{FirstName="Kalvin", LastName="Kleiner", Position="Scientist", EmploymentDate=DateTime.Parse("1993-08-01")},
                new Employee{FirstName="Barney", LastName="Gunson", Position="Security Guard", EmploymentDate=DateTime.Parse("1995-05-03")},
                new Employee{FirstName="Gordon", LastName="Freeman", Position="Theoretical Physicist", EmploymentDate=DateTime.Parse("1995-10-13")}
            };

            employees.ForEach(s => context.Employees.Add(s));
            context.SaveChanges();

            var experiments = new List<Experiment>
            {
                new Experiment{ExperimentID=1150, Title="Tau Cannon Test"},
                new Experiment{ExperimentID=1013, Title="Xen Portal Opening"},
                new Experiment{ExperimentID=880, Title="Sentry Turret Test"}
            };

            experiments.ForEach(s => context.Experiments.Add(s));
            context.SaveChanges();

            var employments = new List<Employment>
            {
                new Employment{EmployeeID=1, ExperimentID=1150, SecurityLevel=SecurityLevel.A},
                new Employment{EmployeeID=2, ExperimentID=880, SecurityLevel=SecurityLevel.C},
                new Employment{EmployeeID=3, ExperimentID=1013, SecurityLevel=SecurityLevel.B}
            };

            employments.ForEach(s => context.Employments.Add(s));
            context.SaveChanges();
        }
    }
}