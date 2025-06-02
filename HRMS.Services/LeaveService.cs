using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Core.Entities;
using HRMS.Core.Interfaces;
using HRMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly HRMSDbContext _context;

        public LeaveService(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaveRequest>> GetAllAsync() =>
            await _context.LeaveRequests.ToListAsync();

        public async Task<List<LeaveRequest>> GetByEmployeeEmailAsync(string email) =>
            await _context.LeaveRequests
                          .Where(l => l.EmployeeEmail == email)
                          .ToListAsync();

        public async Task<LeaveRequest> ApplyAsync(LeaveRequest request)
        {
            _context.LeaveRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<LeaveRequest> ApproveAsync(int id)
        {
            var req = await _context.LeaveRequests.FindAsync(id);
            if (req == null) return null;
            req.Status = LeaveStatus.Approved;
            await _context.SaveChangesAsync();
            return req;
        }

        public async Task<LeaveRequest> RejectAsync(int id)
        {
            var req = await _context.LeaveRequests.FindAsync(id);
            if (req == null) return null;
            req.Status = LeaveStatus.Rejected;
            await _context.SaveChangesAsync();
            return req;
        }
    }

}
