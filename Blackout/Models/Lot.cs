using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blackout.Models.Data
{
    public class Lot
    {
        [Key]
        public required int LotID { get; set; }
        public required int ProductID { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime EntryDate { get; set; }
        [Range(1, int.MaxValue)]
        public required int Amount { get; set; }
        [Precision(10, 3)]
        [Range(0.001, double.MaxValue)]
        public decimal Size { get; set; }
        [MaxLength(50)]
        public required string Location { get; set; }

        public virtual Product Product { get; set; } = null!; // = null! to suppress nullable warning, because it will be loaded by EF
    }
}