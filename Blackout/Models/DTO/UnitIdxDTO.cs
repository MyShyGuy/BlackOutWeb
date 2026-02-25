using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackout.Models.DTO
{
    public class UnitIdxDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "UnitID is required and cannot be empty.")]
        public string UnitID { get; set; } = null!;
        [StringLength(200)]
        public string? Notes { get; set; }
        
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}