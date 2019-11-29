using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework_exercise.Models
{
    public class Experiment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ExperimentID { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Employment> Employments { get; set; }
    }
}