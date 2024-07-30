using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class EmployeeRecognition
    {
        public EmployeeRecognition()
        {
            ModifiedDate = DateTime.Now;
            InsertDate = DateTime.Now;
            Status = "Active";
            RecognitionDate = DateTime.Now;
            RecognizedBy = "";
            RecognitionType = "";
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        [Display(Name = "Employee ID")]
        public int? EmployeeId { get; set; }


        [ForeignKey("EmployeeId")]
        public virtual Employee? Employee { get; set; }

        [Required]
        [Display(Name = "Insert Date")]
        public DateTime InsertDate { get; set; }

        [Required]
        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }


        [Required]
        [MaxLength(50)]
        [Display(Name = "Status")]
        public string Status { get; set; } // e.g., Pending, Approved, Rejected

        [Required]
        [Display(Name = "Recognition Date")]
        public DateTime RecognitionDate { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Recognition Type")]
        public string RecognitionType { get; set; } // e.g., Employee of the Month, Outstanding Performance

        [Required]
        [MaxLength(100)]
        [Display(Name = "Recognized By")]
        public string RecognizedBy { get; set; }

        [MaxLength(500)]
        [Display(Name = "Comments")]
        public string? Comments { get; set; } // Any additional comments
    }
}
