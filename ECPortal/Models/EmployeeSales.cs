using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class EmployeeSales
    {
        public EmployeeSales() {

            ModifiedDate = DateTime.Now;
            InsertDate = DateTime.Now;

        }


        [Key]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeNumber { get; set; }
        public virtual Employee Employee { get; set; }


        // Foreign key for Employee Prepaid Sales
        [ForeignKey("EmployeePrepaidSale")]
        public int EmployeePrepaidSaleId { get; set; }
        public virtual EmployeePrepaidSale EmployeePrepaidSale { get; set; }

        // Foreign key for Employee Postpaid Sales
        [ForeignKey("EmployeePostpaidSale")]
        public int EmployeePostpaidSaleId { get; set; }
        public virtual EmployeePostpaidSale EmployeePostpaidSale { get; set; }

        // Foreign key for Employee Device Sales
        [ForeignKey("EmployeeDeviceSale")]
        public int EmployeeDeviceSaleId { get; set; }
        public virtual EmployeeDeviceSale EmployeeDeviceSale { get; set; }

        // Foreign key for Employee M-Wallet Sales
        [ForeignKey("EmployeeMWalletSale")]
        public int EmployeeMWalletSaleId { get; set; }
        public virtual EmployeeMWalletSale EmployeeMWalletSale { get; set; }

        // Foreign key for Employee 4G Sales
        [ForeignKey("EmployeeFourGSale")]
        public int EmployeeFourGSaleId { get; set; }
        public virtual EmployeeFourGSale EmployeeFourGSale { get; set; }

        // Foreign key for Employee Rox New Sales
        [ForeignKey("EmployeeRoxNewSale")]
        public int EmployeeRoxNewSaleId { get; set; }
        public virtual EmployeeRoxNewSale EmployeeRoxNewSale { get; set; }

        // Foreign key for Employee Rox Conversion Sales
        [ForeignKey("EmployeeRoxConversionSale")]
        public int EmployeeRoxConversionSaleId { get; set; }
        public virtual EmployeeRoxConversionSale EmployeeRoxConversionSale { get; set; }


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
