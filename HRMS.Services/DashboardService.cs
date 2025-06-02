using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Core.Interfaces;
using HRMS.Core.Models;
using HRMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using HRMS.Core.Entities;

namespace HRMS.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly HRMSDbContext _context;

        public DashboardService(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummary> GetDashboardSummaryAsync()
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var summary = new DashboardSummary
            {
                TotalEmployees = await _context.Employees.CountAsync(),
                TotalProjects = await _context.Projects.CountAsync(),
                ActiveLeaves = await _context.LeaveRequests
                    .CountAsync(l => l.Status == LeaveStatus.Approved && l.ToDate >= DateTime.Today),
                TotalAttendanceToday = await _context.AttendanceRecords
                    .CountAsync(a => a.Date.Date == DateTime.Today),

                // ✅ FIXED: Calculate NetPay manually
                TotalMonthlySalary = await _context.Payrolls
                    .Where(p => p.PayDate.Month == currentMonth && p.PayDate.Year == currentYear)
                    .SumAsync(p => p.TotalSalary + p.BonusOrDeduction),

                MonthlyPayrollChart = await _context.Payrolls
                    .GroupBy(p => new { p.PayDate.Year, p.PayDate.Month })
                    .Select(g => new MonthlySalaryChartData
                    {
                        Month = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key.Month)} {g.Key.Year}",
                        TotalSalary = g.Sum(p => p.TotalSalary + p.BonusOrDeduction)
                    })
                    .OrderBy(g => g.Month)
                    .ToListAsync()
            };

            return summary;
        }
    }
}
