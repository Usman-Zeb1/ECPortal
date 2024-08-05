using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class EmployeeFourGSale
    {
        [Key]
        public int Id { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Total { get; set; }

    }
}