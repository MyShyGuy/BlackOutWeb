using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;

namespace Blackout.Models.DTO
{
    public class LotDTO
    {
        public int LotID { get; set; }
        public required int ProductID { get; set; }
        [FutureOrEmpty(ErrorMessage = "Das Ablaufdatum muss entweder leer sein oder mindestens heute oder in der Zukunft liegen.")]
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

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)] // eigene DataAnotations sind möglich, siehe https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationattribute?view=net-8.0
    public class FutureOrEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true; // Leere Werte sind erlaubt

            if (value is DateTime dateTime)
            {
                return dateTime >= DateTime.Today; // Datum muss heute oder in der Zukunft liegen
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} muss entweder leer sein oder mindestens heute oder in der Zukunft liegen.";
        }
    }   
}