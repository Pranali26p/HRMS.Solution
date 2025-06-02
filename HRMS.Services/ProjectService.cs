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
    public class ProjectService : IProjectService
    {
        private readonly HRMSDbContext _context;
        public ProjectService(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllProjectsAsync() =>
            await _context.Projects.ToListAsync();

        public async Task<Project> GetProjectByIdAsync(int id) =>
            await _context.Projects.FindAsync(id);

        public async Task<Project> AddProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var proj = await _context.Projects.FindAsync(id);
            if (proj == null) return false;
            _context.Projects.Remove(proj);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
