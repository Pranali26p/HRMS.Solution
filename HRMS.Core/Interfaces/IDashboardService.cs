using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Core.Models;

namespace HRMS.Core.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummary> GetDashboardSummaryAsync();
    }

}
