using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class ECTNA
    {
        public ECTNA() {

            ModifiedDate = DateTime.Now;
            InsertDate = DateTime.Now;

        }  


        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("EC")]
        [Display(Name = "Experience Center ID")]
        public int? ECID { get; set; }

        public EC EC { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Activity Name")]
        public string ActivityName { get; set; }

      

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // e.g., Pending, Approved, Rejected


        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [MaxLength(500)]
        public string? Comments { get; set; } // Any additional comments

        [Required]
        [Display(Name = "Inserted Date")]
        public DateTime InsertDate { get; set; }

        [Required]
        [Display(Name = "Last Modified Date")]
        public DateTime ModifiedDate { get; set; }
    }
}
