using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackout.Models.DTO
{
    /// <summary>
    /// Minimal information about a unit used for index/list pages.
    /// Includes the UnitID and optional notes, plus navigation to associated products.
    /// </summary>
    public class UnitIdxDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "UnitID is required and cannot be empty.")]
        public string UnitID { get; set; } = null!;
        [StringLength(200)]
        public string? Notes { get; set; }
        
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}