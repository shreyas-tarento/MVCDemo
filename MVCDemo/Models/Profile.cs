using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVCDemo.Models
{
    public class Profile
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public String FirstName { get; set; }
        [StringLength(150)]
        public String LastName { get; set; }
        [NotMappedAttribute]
        public IFormFile Photo { get; set; }
        [StringLength(250)]
        public String PhotoName { get; set; }
        [StringLength(250)]
        public String PhotoPath { get; set; }
    }
}
