using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Core.Entities
{
    public class BonusDeduction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; } // 👈 Foreign Key

        [Required]
        public decimal Amount { get; set; } // Positive = bonus, Negative = deduction

        public string Reason { get; set; }

        public DateTime DateApplied { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
