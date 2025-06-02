using HRMS.Core.Entities;
using HRMS.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _service;

        public LeaveController(ILeaveService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("apply")]
        public async Task<IActionResult> Apply(LeaveRequest request)
        {
            request.EmployeeEmail = User.Identity.Name; // From JWT
            var result = await _service.ApplyAsync(request);
            return Ok(result);
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("my-leaves")]
        public async Task<IActionResult> GetMyLeaves()
        {
            var email = User.Identity.Name;
            var leaves = await _service.GetByEmployeeEmailAsync(email);
            return Ok(leaves);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var leaves = await _service.GetAllAsync();
            return Ok(leaves);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            var result = await _service.ApproveAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> Reject(int id)
        {
            var result = await _service.RejectAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }

}
