using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MainProject.DAL.Data.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ContactNo { get; set; }
        public string EmailID { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }

        [InverseProperty("Employee")]
        public virtual ICollection<Master> Master { get; set; }

    }
}
