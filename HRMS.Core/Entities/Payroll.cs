using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public class Payroll
    {
        public int Id { get; set; }
        public string EmployeeEmail { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public decimal BasicSalary { get; set; }
        public int TotalPresentDays { get; set; }
        public decimal DailyRate { get; set; }
        public decimal TotalSalary { get; set; }

        public DateTime GeneratedOn { get; set; } = DateTime.UtcNow;
    }

}
