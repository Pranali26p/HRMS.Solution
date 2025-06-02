using HRMS.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("mark")]
        public async Task<IActionResult> Mark()
        {
            var email = User.Identity.Name;
            try
            {
                var result = await _attendanceService.MarkAttendanceAsync(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyAttendance()
        {
            var email = User.Identity.Name;
            var data = await _attendanceService.GetMyAttendanceAsync(email);
            return Ok(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _attendanceService.GetAllAttendanceAsync();
            return Ok(data);
        }
    }

}
