using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Core.Entities
{
    public class Payroll
    {
        public int Id { get; set; }


        public int Month { get; set; }
        public int Year { get; set; }

        public decimal BasicSalary { get; set; }
        public int TotalPresentDays { get; set; }
        public decimal DailyRate { get; set; }
        public decimal TotalSalary { get; set; }

        public DateTime GeneratedOn { get; set; } = DateTime.UtcNow;

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        // ✅ ADD THIS: The date this payroll was generated
        public DateTime GeneratedDate { get; set; }
        // ✅ ADD THIS: Bonus or Deduction amount (positive for bonus, negative for deduction)
        public decimal BonusOrDeduction { get; set; }
        public decimal BaseSalary { get; set; }
        public DateTime PayDate { get; set; }
    }
}
