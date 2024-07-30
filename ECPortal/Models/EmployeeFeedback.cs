using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class EmployeeFeedback
    {
        public EmployeeFeedback() {

            ModifiedDate = DateTime.Now;
            InsertDate = DateTime.Now;
            FeedbackDate = DateTime.Now;
            Status = "Pending";

        }



        [Key]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

    
        [MaxLength(50)]
        public string Status { get; set; } // e.g., Pending, Reviewed, Resolved

        [Required]
        public DateTime FeedbackDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string FeedbackType { get; set; } // e.g., Performance, Attendance, Behavior

        [Required]
        [MaxLength(500)]
        public string Feedback { get; set; } // The actual feedback content

        [MaxLength(200)]
        public string? ProvidedBy { get; set; } // Name or ID of the person providing the feedback

        [MaxLength(500)]
        public string? Comments { get; set; } // Any additional comments
    }
}
