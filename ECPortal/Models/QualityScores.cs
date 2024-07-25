namespace Pk.Com.Jazz.ECP.Models
{
    using Pk.Com.Jazz.ECP.Data;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class QualityScores
    {
        public QualityScores() {

            ModifiedDate = DateTime.Now;
            InsertDate = DateTime.Now;

        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EID { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime RecordDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        // Additional columns deemed necessary
        [Required]
        public int Score { get; set; }

        public string? Comments { get; set; }
    }

}
