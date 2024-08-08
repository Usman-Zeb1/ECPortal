using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class ECPrepaidSale
    {

        [Key]
        public int Id { get; set; }


        [Required]
        public int NewSales { get; set; }

        [Required]
        public int PrepaidMNP { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Total {  get; set; }

      

    }
}
