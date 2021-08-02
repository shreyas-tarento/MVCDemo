using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public String FirstName { get; set; }
        [StringLength(150)]
        public String LastName { get; set; }
        [StringLength(150)]
        public String Department { get; set; }
    }
}
