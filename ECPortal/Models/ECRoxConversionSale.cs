using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class ECRoxConversionSale
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public int BasicVibe { get; set; }

        [Required]
        public int CrazyVibe { get; set; }

        [Required]
        public int InsaneVibe { get; set; }

        [Required]
        public int NoVibe { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Total { get; set; }


    }
}