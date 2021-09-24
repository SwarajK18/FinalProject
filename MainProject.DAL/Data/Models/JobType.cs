using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MainProject.DAL.Data.Models
{
    public class JobType
    {
        [Key]
        public int ID { get; set; }
        public string JobTypeName { get; set; }

        [InverseProperty("JobType")]
        public virtual ICollection<Master> Master { get; set; }
    }
}
