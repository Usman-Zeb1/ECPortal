using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class ECDeviceSale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MBB { get; set; }

        [Required]
        public int Handsets { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Total { get; set; }

    }
}
