using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public class EmployeeSalaryConfig
    {
        public int Id { get; set; }
        public string EmployeeEmail { get; set; }
        public decimal BasicMonthlySalary { get; set; }
    }

}
