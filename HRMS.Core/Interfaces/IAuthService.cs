using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(string name, string email, string password, string role);
        Task<string> LoginAsync(string email, string password);
    }

}
