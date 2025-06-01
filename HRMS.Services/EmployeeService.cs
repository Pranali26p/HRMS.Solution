using HRMS.Core.Entities;
using HRMS.Core.Interfaces;

namespace HRMS.Services
{
    public class EmployeeService
    {
        private readonly IRepository<Employee> _employeeRepo;

        public EmployeeService(IRepository<Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public Task<IEnumerable<Employee>> GetAllEmployeesAsync() => _employeeRepo.GetAllAsync();
        public Task AddEmployeeAsync(Employee emp) => _employeeRepo.AddAsync(emp);
        public Task UpdateEmployeeAsync(Employee emp) => _employeeRepo.UpdateAsync(emp);
        public Task<Employee> GetEmployeeByIdAsync(int id) => _employeeRepo.GetByIdAsync(id);
        public Task DeleteEmployeeAsync(int id) => _employeeRepo.DeleteAsync(id);
    }
}
