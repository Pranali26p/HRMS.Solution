using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Core.Interfaces;
using HRMS.Core.Models;
using HRMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services
{
    public class AppraisalService : IAppraisalService
    {
        private readonly HRMSDbContext _context;

        public AppraisalService(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<List<Appraisal>> GetAllAsync() =>
            await _context.Appraisals.Include(a => a.Employee).ToListAsync();

        public async Task<Appraisal> GetByIdAsync(int id) =>
            await _context.Appraisals.Include(a => a.Employee).FirstOrDefaultAsync(a => a.Id == id);

        public async Task<List<Appraisal>> GetByEmployeeIdAsync(int empId) =>
            await _context.Appraisals.Where(a => a.EmployeeId == empId).ToListAsync();

        public async Task<Appraisal> AddAsync(Appraisal appraisal)
        {
            _context.Appraisals.Add(appraisal);
            await _context.SaveChangesAsync();
            return appraisal;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var appraisal = await _context.Appraisals.FindAsync(id);
            if (appraisal == null) return false;
            _context.Appraisals.Remove(appraisal);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
