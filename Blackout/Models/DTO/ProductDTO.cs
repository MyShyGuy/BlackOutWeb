using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackout.Models.DTO
{
    /// <summary>
    /// Data transfer object for product creation/edit forms.
    /// The server returns products to the UI using this shape, hiding internal metadata.
    /// </summary>
    public class ProductDTO
    {
        public int ProductID { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public required string Title { get; set; }
        [MaxLength(500)]
        public string? Notes { get; set; }
        [Range(0, 2147483647, ErrorMessage = "Maximum of 10 digits allowed.")]
        public long EAN { get; set; }
        [StringLength(10, MinimumLength = 1)]
        public required string UnitID { get; set; }
    }
}