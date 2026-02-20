using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackout.Models.Data
{
    public class Unit
    {
        [Key]
        [StringLength(10, MinimumLength = 1)]
        public required string UnitID { get; set; } = null!;
        [StringLength(200)]
        public string? Notes { get; set; }
        
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}