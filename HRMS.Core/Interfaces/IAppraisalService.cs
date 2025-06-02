using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Core.Models;

namespace HRMS.Core.Interfaces
{
    public interface IAppraisalService
    {
        Task<List<Appraisal>> GetAllAsync();
        Task<Appraisal> GetByIdAsync(int id);
        Task<List<Appraisal>> GetByEmployeeIdAsync(int empId);
        Task<Appraisal> AddAsync(Appraisal appraisal);
        Task<bool> DeleteAsync(int id);
    }

}
