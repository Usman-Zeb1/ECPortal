using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class EmployeeCommission
    {
        public EmployeeCommission() {

            ModifiedDate = DateTime.Now;
            InsertDate = DateTime.Now;

        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // e.g., Pending, Approved, Rejected

        [Required]
        public DateTime CommissionDate { get; set; }

        [Required]
        public decimal CommissionAmount { get; set; }

        [MaxLength(500)]
        public string? Comments { get; set; } // Any additional comments
    }
}
