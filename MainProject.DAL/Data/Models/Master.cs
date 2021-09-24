using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MainProject.DAL.Data.Models
{
    public class Master
    {
        public int ID { get; set; }

        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }

        [Display(Name = "SuperVisior")]
        public int SuperVisorId { get; set; }

        [Display(Name = "JobType")]
        public int JobTypeId { get; set; }

        [Display(Name = "Start Date & Time")]
        public DateTime Start { get; set; }

        [Display(Name = "End Date & Time")]
        public DateTime End { get; set; }

        [Display(Name = "Location")]
        public int LocationId { get; set; }

        public string Comment { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("Master")]
        public virtual Employee Employee { get; set; }

        [ForeignKey(nameof(SuperVisorId))]
        [InverseProperty("Master")]
        public virtual SuperVisor SuperVisor { get; set; }

        [ForeignKey(nameof(JobTypeId))]
        [InverseProperty("Master")]
        public virtual JobType JobType { get; set; }

        [ForeignKey(nameof(LocationId))]
        [InverseProperty("Master")]
        public virtual Location Location { get; set; }
    }
}
