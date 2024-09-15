using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Warehouse
    {
      
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // should to be unique

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]   
        public int UserId { get; set; }  // Foreign key to the User
        
        public Users User { get; set; }

        public DateTime CreatedDate { get; set; }



    }
}
