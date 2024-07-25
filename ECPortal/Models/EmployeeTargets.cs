using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class EmployeeTargets
    {
        public EmployeeTargets() {

            ModifiedDate = DateTime.Now;
            InsertDate = DateTime.Now;

        }


        [Key]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        [Display(Name = "RCCH Region (City/Location)")]
        [MaxLength(500)]
        public string RCCH_Region { get; set; }

        [Required]
        [Display(Name = "Business Center")]
        [MaxLength(500)]
        public string Business_Center { get; set; }

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
        [Display(Name = "Target Start Date")]
        public DateTime TargetStartDate { get; set; }

        [Required]
        [Display(Name = "Target End Date")]
        public DateTime TargetEndDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Target Amount")]
        public decimal TargetAmount { get; set; }

        [MaxLength(500)]
        public string? Comments { get; set; } // Any additional comments
    }
}
