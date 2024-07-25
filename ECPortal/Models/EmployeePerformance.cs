using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class EmployeePerformance
    {

        public EmployeePerformance() {

            ModifiedDate = DateTime.Now;
            InsertDate = DateTime.Now;

        }


        [Key]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        [Display(Name = "Inserted Date")]
        public DateTime InsertDate { get; set; }

        [Required]
        [Display(Name = "Last Modified Date")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // e.g., Pending, Approved, Rejected

        [Required]
        [Display(Name = "Performance Start Date")]
        public DateTime PerformanceStartDate { get; set; }

        [Required]
        [Display(Name = "Performance End Date")]
        public DateTime PerformanceEndDate { get; set; }

        [Required]
        [Display(Name = "Performance Score")]
        public int PerformanceScore { get; set; }

        [MaxLength(500)]
        public string? Comments { get; set; } // Any additional comments
    }
}
