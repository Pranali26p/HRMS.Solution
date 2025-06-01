using HRMS.Core.Entities;
using HRMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeesController(EmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllEmployeesAsync());

        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            await _service.AddEmployeeAsync(emp);
            return Ok("Created");
        }
    }

}
