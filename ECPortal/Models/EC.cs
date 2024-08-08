using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class EC
    {
        public EC() {

            LastModifiedDate = DateTime.Now;
            CreationDate = DateTime.Now;

        }

        [Key]
        [Display(Name = "Experience Center ID")]
        public int ECID { get; set; }

        [Required]
        [ForeignKey("ECRegionID")]
        public int ECRegionID {  get; set; }

     
        [Required]
        [MaxLength(200)]
        [Display(Name = "Address")]
        public string PhysicalAddress { get; set; }

        [Required]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [Required]
        [Display(Name = "Last Modified Date")]
        public DateTime LastModifiedDate { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Status")]
        public string OperationalStatus { get; set; } // e.g., Operational, Under Renovation, Closed
        public ICollection<ECAudits> ECAudits { get; set; }
        public ICollection<ECGiveaways> ECGiveaways { get; set; }
        public ICollection<ECStocks> ECStocks { get; set; }
        public ICollection<ECTNA> ECTNAs { get; set; }
        public ICollection<Employee> Employees { get; set; }

        public virtual ECRegions ECRegion { get; set; }
    }
}
