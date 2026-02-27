using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;

namespace Blackout.Models.DTO
{
    /// <summary>
    /// Data transfer object for a lot.  Used by the UI when creating or editing lots.
    /// Contains only the properties the client needs, and validation attributes.
    /// </summary>
    public class LotDTO
    {
        public int LotID { get; set; }
        public required int ProductID { get; set; }
        [FutureOrEmpty(ErrorMessage = "Expiry date must be empty or today or in the future.")]
        public DateTime ExpiryDate { get; set; }
        public DateTime EntryDate { get; set; }
        [Range(1, int.MaxValue)]
        public required int Amount { get; set; }
        [Precision(10, 3)]
        [Range(0.001, double.MaxValue)]
        public decimal Size { get; set; }
        [MaxLength(50)]
        public required string Location { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)] // custom validation attribute; see https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationattribute?view=net-8.0
    public class FutureOrEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true; // empty values are permitted

            if (value is DateTime dateTime)
            {
                return dateTime >= DateTime.Today; // date must be today or in the future
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be empty or today or a future date.";
        }
    }   
}