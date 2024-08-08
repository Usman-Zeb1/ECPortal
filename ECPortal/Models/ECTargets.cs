using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pk.Com.Jazz.ECP.Models
{
    public class ECTargets
    {
        public ECTargets()
        {

            InsertDate = DateTime.Now;
            ModifiedDate = DateTime.Now;

        }


        [Key]
        public int Id { get; set; }

        [ForeignKey("EC")]
        [Column("ECID")]
        public int ECID { get; set; }
        public virtual EC EC { get; set; }

        public int ECPrepaidSaleTarget { get; set; }
        public int ECPostpaidSaleTarget { get; set; }
        public int ECDeviceSaleTarget { get; set; }
        public int ECMWalletSaleTarget { get; set; }
        public int ECFourGSaleTarget { get; set; }
        public int ECRoxNewSaleTarget { get; set; }
        public int ECRoxConversionSaleTarget { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public DateTime InsertDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } 


    }
}
