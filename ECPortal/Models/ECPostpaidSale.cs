using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class ECPostpaidSale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FreshSales { get; set; }

        [Required]
        public int PortIN { get; set; }

        [Required]
        public int PreToPost { get; set; }

        [Required]
        public int RedRedZ { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Total { get; set; }

    }
}
