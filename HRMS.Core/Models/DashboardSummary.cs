using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Models
{
    public class DashboardSummary
    {
        public int TotalEmployees { get; set; }
        public int TotalProjects { get; set; }
        public int ActiveLeaves { get; set; }
        public int TotalAttendanceToday { get; set; }
        public decimal TotalMonthlySalary { get; set; }

        public List<MonthlySalaryChartData> MonthlyPayrollChart { get; set; }
    }

    public class MonthlySalaryChartData
    {
        public string Month { get; set; }
        public decimal TotalSalary { get; set; }
    }

}
