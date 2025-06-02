using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Core.Entities;

namespace HRMS.Core.Interfaces
{
    public interface IPayrollService
    {
        Task SetSalaryConfigAsync(int employeeId, decimal salary);
        Task<Payroll> GeneratePayrollAsync(int employeeId, int month, int year);
        Task<List<Payroll>> GetPayrollByEmployeeAsync(int employeeId);
        Task<List<Payroll>> GetAllPayrollsAsync();
        byte[] GeneratePdf(Payroll payroll);
        byte[] GenerateExcel(Payroll payroll);
    }

}
