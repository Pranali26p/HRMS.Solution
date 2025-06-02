using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class LeaveRequest
    {
        public int Id { get; set; }
        public string EmployeeEmail { get; set; } // identify employee via email
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Reason { get; set; }
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    }

}
