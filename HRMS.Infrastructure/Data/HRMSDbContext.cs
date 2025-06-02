using HRMS.Core.Entities;
using HRMS.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Infrastructure.Data
{
    public class HRMSDbContext : DbContext
    {
        public HRMSDbContext(DbContextOptions<HRMSDbContext> options)
            : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<EmployeeSalaryConfig> SalaryConfigs { get; set; }
        public DbSet<BonusDeduction> BonusDeductions { get; set; }
        public DbSet<Appraisal> Appraisals { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships, constraints, etc. (optional for now)
            modelBuilder.Entity<Employee>().ToTable("Employees");
        }
    }
}
