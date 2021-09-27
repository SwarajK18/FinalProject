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

        [Required(ErrorMessage = "Please Select Employee Name")]
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Please Select SuperVisor Name")]
        [Display(Name = "SuperVisior")]
        public int SuperVisorId { get; set; }
        [Required(ErrorMessage = "Please Select Job Type")]
        [Display(Name = "JobType")]
        public int JobTypeId { get; set; }
        [Required(ErrorMessage = "Please Select Start Date & Time")]
        [Display(Name = "Start Date & Time")]
        public DateTime Start { get; set; }
        [Required(ErrorMessage = "Please Select End Date & Time")]
        [Display(Name = "End Date & Time")]
        public DateTime End { get; set; }
        [Required(ErrorMessage = "Please Select Location")]
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
