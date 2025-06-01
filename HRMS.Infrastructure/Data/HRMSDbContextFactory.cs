using HRMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HRMS.Infrastructure
{
    public class HRMSDbContextFactory : IDesignTimeDbContextFactory<HRMSDbContext>
    {
        public HRMSDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HRMSDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=HRMS_F_DB;Trusted_Connection=True;TrustServerCertificate=True;");

            return new HRMSDbContext(optionsBuilder.Options);
        }
    }
}
