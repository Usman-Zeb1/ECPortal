using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class QuizScores
    {
        public QuizScores() {

            ModifiedDate = DateTime.Now;
            InsertDate = DateTime.Now;

        }


        [Key]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime QuizDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        // Additional columns deemed necessary
        [Required]
        public int Score { get; set; }

        public string? Comments { get; set; }
    }
}
