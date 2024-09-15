using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } // should to be unique

        public string? SKUCode { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Cost { get; set; }

        public double? MSRPPrice { get; set; }
        
        [Required]
        public int WarehouseId { get; set; } // Foreign key to the Warehouse
        public Warehouse Warehouse { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}
