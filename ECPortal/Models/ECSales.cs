using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class ECSales
    {
        public ECSales() {

            ModifiedDate = DateTime.Now;
            InsertDate = DateTime.Now;

        }


        [Key]
        public int Id { get; set; }

        [ForeignKey("EC")]
        public int ECID { get; set; }
        public virtual EC EC { get; set; }


        // Foreign key for EC Prepaid Sales
        [ForeignKey("ECPrepaidSale")]
        public int ECPrepaidSaleId { get; set; }
        public virtual ECPrepaidSale ECPrepaidSale { get; set; }

        // Foreign key for EC Postpaid Sales
        [ForeignKey("ECPostpaidSale")]
        public int ECPostpaidSaleId { get; set; }
        public virtual ECPostpaidSale ECPostpaidSale { get; set; }

        // Foreign key for EC Device Sales
        [ForeignKey("ECDeviceSale")]
        public int ECDeviceSaleId { get; set; }
        public virtual ECDeviceSale ECDeviceSale { get; set; }

        // Foreign key for EC M-Wallet Sales
        [ForeignKey("ECMWalletSale")]
        public int ECMWalletSaleId { get; set; }
        public virtual ECMWalletSale ECMWalletSale { get; set; }

        // Foreign key for EC 4G Sales
        [ForeignKey("ECFourGSale")]
        public int ECFourGSaleId { get; set; }
        public virtual ECFourGSale ECFourGSale { get; set; }

        // Foreign key for EC Rox New Sales
        [ForeignKey("ECRoxNewSale")]
        public int ECRoxNewSaleId { get; set; }
        public virtual ECRoxNewSale ECRoxNewSale { get; set; }

        // Foreign key for EC Rox Conversion Sales
        [ForeignKey("ECRoxConversionSale")]
        public int ECRoxConversionSaleId { get; set; }
        public virtual ECRoxConversionSale ECRoxConversionSale { get; set; }


        [Required]
        public DateTime InsertDate { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [Required]
        public DateTime SalesDate { get; set; }

    }
}
