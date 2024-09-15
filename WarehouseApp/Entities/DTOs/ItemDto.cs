using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string? SKUCode { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public double? MSRPPrice { get; set; }
    }
}
