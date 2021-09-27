using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MainProject.DAL.Data.Models
{
    public class Employee
    {
        public Employee()
        {
            Employees = new HashSet<Employee>();
        }
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Please provide Your Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 12)]
        public string ContactNo { get; set; }
        public string EmailID { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }

        public int LocationsID { get; set; }
        public int JobTypesID { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("Employee")]
        public virtual ICollection<Master> Master { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

    }
}
