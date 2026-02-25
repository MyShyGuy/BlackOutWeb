using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blackout.Models.Data
{
    [Table("Products",Schema = "prd")]
    // [PrimaryKey(nameof(ProductID), nameof(Title))]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public required string Title { get; set; }
        [MaxLength(500)]
        public string? Notes { get; set; }
        [Range(0,9999999999999999999, ErrorMessage = "Nur Zahlen erlaubt")]
        public long EAN { get; set; }
        [StringLength(10, MinimumLength = 1)]
        public required string UnitID { get; set; }
        // virtual for lazy loading
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Unit Unit { get; set; } = null!; // = null! to suppress nullable warning, because it will be loaded by EF
        public virtual ICollection<Lot>? Lots { get; set; } = new List<Lot>();
    }
}