using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Entities
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public string EmployeeEmail { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow.Date;
        public DateTime TimeIn { get; set; } = DateTime.UtcNow;
    }

}
