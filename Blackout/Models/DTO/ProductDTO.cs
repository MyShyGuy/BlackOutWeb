using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackout.Models.DTO
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public required string Title { get; set; }
        [MaxLength(500)]
        public string? Notes { get; set; }
        [Range(0, 2147483647, ErrorMessage = "Maximal 10 Stellen erlaubt.")]
        public long EAN { get; set; }
        [StringLength(10, MinimumLength = 1)]
        public required string UnitID { get; set; }
    }
}