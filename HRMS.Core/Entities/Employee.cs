using System.ComponentModel.DataAnnotations;

namespace HRMS.Core.Entities
{
    public class Employee
    {
        [Key] // ✅ Now Id is the primary key
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } // ✅ Keep Email as unique if needed

        public string FullName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public DateTime JoinDate { get; set; }
        public decimal Salary { get; set; }
    }
}
