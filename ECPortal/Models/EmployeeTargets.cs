using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class EmployeeTargets
    {
        public EmployeeTargets() {

            InsertDate = DateTime.Now;
            ModifiedDate = DateTime.Now;

        }


        [Key]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        [Column("EmployeeNumber")]
        public int EmployeeNumber { get; set; }
        public virtual Employee Employee { get; set; }

        public int EmployeePrepaidSaleTarget { get; set; }
        public int EmployeePostpaidSaleTarget { get; set; }
        public int EmployeeDeviceSaleTarget { get; set; }
        public int EmployeeMWalletSaleTarget { get; set; }
        public int EmployeeFourGSaleTarget { get; set; }
        public int EmployeeRoxNewSaleTarget { get; set; }
        public int EmployeeRoxConversionSaleTarget { get; set; }

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
        public string Status { get; set; } // e.g., Pending, Approved, Rejected

      
    }
}
