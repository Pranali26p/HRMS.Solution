using HRMS.Core.Entities;
using HRMS.Core.Interfaces;
using HRMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HRMSDbContext _context;

        public EmployeeService(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync() =>
            await _context.Employees.ToListAsync();

        public async Task<Employee> GetByIdAsync(int id) =>
            await _context.Employees.FindAsync(id);

        public async Task<Employee> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateAsync(int id, Employee emp)
        {
            var existing = await _context.Employees.FindAsync(id);
            if (existing == null) return null;

            existing.FullName = emp.FullName;
            existing.Email = emp.Email;
            existing.Department = emp.Department;
            existing.Position = emp.Position;
            existing.JoinDate = emp.JoinDate;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteAsync(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                await _context.SaveChangesAsync();
            }
        }
    }

}
