using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class ECGiveaways
    {
        public ECGiveaways()
        {
            ModifiedDate = DateTime.Now;
            InsertDate = DateTime.Now;
            Status = "Available"; // Set default status
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("EC")]
        [Display(Name = "Experience Center ID")]
        public int? ECID { get; set; }

        [Required]
        [Display(Name = "Inserted Date")]
        public DateTime InsertDate { get; set; }

        [Required]
        [Display(Name = "Last Modified Date")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Availability { get; set; } // e.g., Available, Not Available

        [Required]
        [MaxLength(100)]
        [Display(Name = "Giveaway Item")]
        public string? GiveawayItem { get; set; }

        [Required]
        [Display(Name = "Quantity Available")]
        public int QuantityAvailable { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; } // Any additional comments or description
    }
}
