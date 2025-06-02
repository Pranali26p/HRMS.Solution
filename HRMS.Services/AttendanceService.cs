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
    public class AttendanceService : IAttendanceService
    {
        private readonly HRMSDbContext _context;

        public AttendanceService(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<AttendanceRecord> MarkAttendanceAsync(string email)
        {
            var today = DateTime.UtcNow.Date;

            // Prevent duplicate attendance
            var alreadyMarked = await _context.AttendanceRecords
                .AnyAsync(a => a.EmployeeEmail == email && a.Date == today);

            if (alreadyMarked)
                throw new Exception("Attendance already marked for today.");

            var record = new AttendanceRecord
            {
                EmployeeEmail = email,
                Date = today,
                TimeIn = DateTime.UtcNow
            };

            _context.AttendanceRecords.Add(record);
            await _context.SaveChangesAsync();

            return record;
        }

        public async Task<List<AttendanceRecord>> GetMyAttendanceAsync(string email)
        {
            return await _context.AttendanceRecords
                .Where(a => a.EmployeeEmail == email)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<List<AttendanceRecord>> GetAllAttendanceAsync()
        {
            return await _context.AttendanceRecords
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }
    }

}
