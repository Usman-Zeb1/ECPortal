using System;
using System.ComponentModel.DataAnnotations;

namespace Pk.Com.Jazz.ECP.Models
{
    public class TrainingRequests
    {
        public TrainingRequests()
        {
            RequestDate = DateTime.Now;
            SubmissionDate = DateTime.Now;
            Status = "Pending";
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TrainingName { get; set; }

        [Required]
        public string RequestDetails { get; set; }

        [Required]
        public DateTime RequestDate { get; set; } // Date when the training is requested

        [Required]
        public DateTime SubmissionDate { get; set; } // Date when the form was submitted

        public DateTime? ApprovedDate { get; set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; set; }

        [MaxLength(50)]
        public string? Comments { get; set; }
    }
}
