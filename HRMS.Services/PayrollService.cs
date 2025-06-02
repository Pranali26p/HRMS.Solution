using HRMS.Core.Entities;
using HRMS.Core.Interfaces;
using HRMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Infrastructure.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly HRMSDbContext _context;

        public PayrollService(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task SetSalaryConfigAsync(int employeeId, decimal salary)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                employee.Salary = salary;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Payroll> GeneratePayrollAsync(int employeeId, int month, int year)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null) return null;

            var bonuses = await _context.BonusDeductions
                .Where(b => b.EmployeeId == employeeId && b.DateApplied.Month == month && b.DateApplied.Year == year)
                .ToListAsync();

            decimal totalBonusOrDeduction = bonuses.Sum(b => b.Amount);
            decimal totalSalary = employee.Salary + totalBonusOrDeduction;

            var payroll = new Payroll
            {
                EmployeeId = employeeId,
                Month = month,
                Year = year,
                BaseSalary = employee.Salary,
                BonusOrDeduction = totalBonusOrDeduction,
                TotalSalary = totalSalary,
                GeneratedDate = DateTime.UtcNow
            };

            _context.Payrolls.Add(payroll);
            await _context.SaveChangesAsync();

            return payroll;
        }

        public async Task<List<Payroll>> GetPayrollByEmployeeAsync(int employeeId)
        {
            return await _context.Payrolls
                .Where(p => p.EmployeeId == employeeId)
                .OrderByDescending(p => p.GeneratedDate)
                .ToListAsync();
        }

        public async Task<List<Payroll>> GetAllPayrollsAsync()
        {
            return await _context.Payrolls
                .Include(p => p.Employee)
                .OrderByDescending(p => p.GeneratedDate)
                .ToListAsync();
        }

        public byte[] GeneratePdf(Payroll payroll)
        {
            // Stub: Implement using PDF library like iTextSharp or PdfSharp
            var content = $"Salary Slip for {payroll.Month}/{payroll.Year}\nTotal Salary: {payroll.TotalSalary}";
            return System.Text.Encoding.UTF8.GetBytes(content);
        }

        public byte[] GenerateExcel(Payroll payroll)
        {
            // Stub: Implement using EPPlus or ClosedXML
            var content = $"EmployeeID,Month,Year,TotalSalary\n{payroll.EmployeeId},{payroll.Month},{payroll.Year},{payroll.TotalSalary}";
            return System.Text.Encoding.UTF8.GetBytes(content);
        }
    }
}
