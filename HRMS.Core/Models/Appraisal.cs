using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Core.Entities;

namespace HRMS.Core.Models
{
    public class Appraisal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [Range(1, 10)]
        public int Rating { get; set; }  // Scale of 1 to 10

        [MaxLength(500)]
        public string ReviewComments { get; set; }

        public DateTime AppraisalDate { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string AppraisedBy { get; set; }  // Admin name
    }

}
