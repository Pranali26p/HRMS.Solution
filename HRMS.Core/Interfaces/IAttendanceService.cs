using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Core.Entities;

namespace HRMS.Core.Interfaces
{
    public interface IAttendanceService
    {
        Task<AttendanceRecord> MarkAttendanceAsync(string email);
        Task<List<AttendanceRecord>> GetMyAttendanceAsync(string email);
        Task<List<AttendanceRecord>> GetAllAttendanceAsync();
    }
}
