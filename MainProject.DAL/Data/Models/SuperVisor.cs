using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MainProject.DAL.Data.Models
{
   public class SuperVisor
    {
        [Key]
        public int ID { get; set; }
        public string SuperVName { get; set; }

        [InverseProperty("SuperVisor")]
        public virtual ICollection<Master> Master { get; set; }
    }
}
