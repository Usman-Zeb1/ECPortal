using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class EmployeeDeviceSale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MBB { get; set; }

        [Required]
        public int Handsets { get; set; }

        [Required]
        public int Target { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Total { get; set; }

    }
}
