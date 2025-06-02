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
    public class ProjectAssignmentService : IProjectAssignmentService
    {
        private readonly HRMSDbContext _context;
        public ProjectAssignmentService(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectAssignment>> GetAllAssignmentsAsync() =>
            await _context.ProjectAssignments
                          .Include(p => p.Employee)
                          .Include(p => p.Project)
                          .ToListAsync();

        public async Task<List<ProjectAssignment>> GetAssignmentsByEmployeeIdAsync(int empId) =>
            await _context.ProjectAssignments
                          .Include(p => p.Project)
                          .Where(p => p.EmployeeId == empId)
                          .ToListAsync();

        public async Task<ProjectAssignment> AssignProjectAsync(ProjectAssignment assignment)
        {
            _context.ProjectAssignments.Add(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }

        public async Task<bool> RemoveAssignmentAsync(int id)
        {
            var assign = await _context.ProjectAssignments.FindAsync(id);
            if (assign == null) return false;
            _context.ProjectAssignments.Remove(assign);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
