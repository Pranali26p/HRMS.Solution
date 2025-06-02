using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Core.Entities;

namespace HRMS.Core.Interfaces
{
    public interface ILeaveService
    {
        Task<List<LeaveRequest>> GetAllAsync();
        Task<List<LeaveRequest>> GetByEmployeeEmailAsync(string email);
        Task<LeaveRequest> ApplyAsync(LeaveRequest request);
        Task<LeaveRequest> ApproveAsync(int id);
        Task<LeaveRequest> RejectAsync(int id);
    }

}
