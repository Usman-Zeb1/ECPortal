using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class ECAudits
    {
        public ECAudits() {

            ModifiedDate = DateTime.Now;
            InsertDate = DateTime.Now;


        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("EC")]
        [Display(Name = "Experience Center ID")]
        public int? ECID { get; set; }


        [ForeignKey("ECID")]
        public virtual EC? EC { get; set; }

        [Required]
        [Display(Name = "Audit Date")]
        public DateTime AuditDate { get; set; }

        [Required]
        [Display(Name = "Auditor")]
        public string? Auditor { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Audited Area")]
        public string? AuditedArea { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Findings { get; set; } // e.g., Observations, Issues

        [MaxLength(500)]
        public string? Actions { get; set; } // Any corrective actions taken

        [MaxLength(500)]
        public string? Comments { get; set; } // Any additional comments or notes

        [Required]
        [Display(Name = "Inserted Date")]
        public DateTime InsertDate { get; set; }

        [Required]
        [Display(Name = "Last Modified Date")]
        public DateTime ModifiedDate { get; set; }
    }
}
