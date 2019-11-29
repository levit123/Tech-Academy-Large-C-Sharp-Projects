using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityFramework_exercise.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EntityFramework_exercise.DAL
{
    public class BlackMesaContext : DbContext
    {
        public BlackMesaContext() : base("BlackMesaContext")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<Experiment> Experiments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}