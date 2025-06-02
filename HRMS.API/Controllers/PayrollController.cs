using HRMS.Core.Entities;
using HRMS.Core.Interfaces;
using HRMS.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.IO;

namespace HRMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;
        private readonly HRMSDbContext _context;

        public PayrollController(IPayrollService payrollService, HRMSDbContext context)
        {
            _payrollService = payrollService;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("setsalary")]
        public async Task<IActionResult> SetSalary([FromQuery] string email, [FromQuery] decimal salary)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
            if (employee == null) return NotFound("Employee not found.");

            await _payrollService.SetSalaryConfigAsync(employee.Id, salary);
            return Ok("Salary config saved.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("generate")]
        public async Task<IActionResult> GeneratePayroll([FromQuery] string email, [FromQuery] int month, [FromQuery] int year)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
            if (employee == null) return NotFound("Employee not found.");

            var payroll = await _payrollService.GeneratePayrollAsync(employee.Id, month, year);
            return Ok(payroll);
        }


        [Authorize(Roles = "Employee")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyPayroll()
        {
            var email = User.Identity.Name;
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
            if (employee == null) return NotFound("Employee not found.");

            var data = await _payrollService.GetPayrollByEmployeeAsync(employee.Id);
            return Ok(data);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPayrolls()
        {
            var data = await _payrollService.GetAllPayrollsAsync();
            return Ok(data);
        }

        [HttpGet("export/pdf")]
        public async Task<IActionResult> ExportPdf([FromQuery] int payrollId)
        {
            var payroll = await _context.Payrolls.FindAsync(payrollId);
            if (payroll == null) return NotFound("Payroll not found.");

            var pdfBytes = _payrollService.GeneratePdf(payroll);
            return File(pdfBytes, "application/pdf", "SalarySlip.pdf");
        }

        [HttpGet("export/excel")]
        public async Task<IActionResult> ExportExcel([FromQuery] int payrollId)
        {
            var payroll = await _context.Payrolls.FindAsync(payrollId);
            if (payroll == null) return NotFound("Payroll not found.");

            var excelBytes = _payrollService.GenerateExcel(payroll);
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalarySlip.xlsx");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("email-salary-slip")]
        public async Task<IActionResult> EmailSalarySlip([FromQuery] int payrollId)
        {
            var payroll = await _context.Payrolls.Include(p => p.Employee).FirstOrDefaultAsync(p => p.Id == payrollId);
            if (payroll == null) return NotFound("Payroll not found.");

            if (string.IsNullOrEmpty(payroll.Employee?.Email))
                return BadRequest("Employee email not found.");

            var pdfBytes = _payrollService.GeneratePdf(payroll);

            using var smtp = new SmtpClient("smtp.yourdomain.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("your-email@domain.com", "your-email-password"),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress("your-email@domain.com"),
                Subject = "Your Salary Slip",
                Body = "Attached is your salary slip.",
                IsBodyHtml = false
            };

            mail.To.Add(payroll.Employee.Email);
            mail.Attachments.Add(new Attachment(new MemoryStream(pdfBytes), "SalarySlip.pdf", "application/pdf"));

            await smtp.SendMailAsync(mail);
            return Ok("Salary slip sent.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-bonus-deduction")]
        public async Task<IActionResult> AddBonusOrDeduction([FromQuery] string email, [FromQuery] decimal amount, [FromQuery] string reason)
        {
            // Find employee by email
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
            if (employee == null) return NotFound("Employee not found.");

            var record = new BonusDeduction
            {
                EmployeeId = employee.Id, // Correct FK assignment
                Amount = amount,
                Reason = reason,
                DateApplied = DateTime.UtcNow
            };

            _context.BonusDeductions.Add(record);
            await _context.SaveChangesAsync();
            return Ok("Bonus/Deduction added.");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("chart-data")]
        public IActionResult GetMonthlySalaryDistribution([FromQuery] int year)
        {
            var chartData = _context.Payrolls
                .Where(p => p.Year == year)
                .GroupBy(p => p.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalSalary = g.Sum(p => p.TotalSalary)
                })
                .OrderBy(x => x.Month)
                .ToList();

            return Ok(chartData);
        }
    }
}
