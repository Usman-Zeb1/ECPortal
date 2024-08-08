using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class ECRegions
    {
     

        [Key]
        [Display(Name = "Experience Center Region ID")]
        public int ECRegionID { get; set; }

        [Required]
        [MaxLength(100)]
        public string ECRegionName { get; set; }

        [Required]
        [MaxLength(100)]
        public string ECParentRegionName { get; set; }

        public ICollection<EC> ECs { get; set; }


    }
}
