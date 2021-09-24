using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MainProject.DAL.Data.Models
{
    public class Location
    {
       [Key]
        public int ID { get; set; }

        public string LocationName { get; set; }

        [InverseProperty("Location")]
        public virtual ICollection<Master> Master { get; set; }

    }
}
