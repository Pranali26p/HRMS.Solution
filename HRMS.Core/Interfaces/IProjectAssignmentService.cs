using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Core.Entities;

namespace HRMS.Core.Interfaces
{
    public interface IProjectAssignmentService
    {
        Task<List<ProjectAssignment>> GetAllAssignmentsAsync();
        Task<List<ProjectAssignment>> GetAssignmentsByEmployeeIdAsync(int empId);
        Task<ProjectAssignment> AssignProjectAsync(ProjectAssignment assignment);
        Task<bool> RemoveAssignmentAsync(int id);
    }

}
