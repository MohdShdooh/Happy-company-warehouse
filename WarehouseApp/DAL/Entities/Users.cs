using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; } // should to be unique

        [Required]
        public string Password { get; set; } 

        [Required]
        public int Role {  get; set; }

        [Required]
        public int Status { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
