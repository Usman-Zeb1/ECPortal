using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class EmployeeEDA
    {

        public EmployeeEDA() {

            ModifiedDate = DateTime.Now;
            InsertDate = DateTime.Now;

        }


        [Key]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        [Required]
        [MaxLength(100)]
        public string EDAName { get; set; } // e.g., Orientation, Onboarding, Training

        [Required]
        public DateTime InsertDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // e.g., Pending, Reviewed, Resolved

        [Required]
        public DateTime EDAStartDate { get; set; }

        [Required]
        public DateTime EDAEndDate { get; set; }

        

        [MaxLength(500)]
        public string? Comments { get; set; } // Any additional comments
    }
}
