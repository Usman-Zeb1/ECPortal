using Pk.Com.Jazz.ECP.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pk.Com.Jazz.ECP.Models
{
    public class ManagersScores
    {
        public ManagersScores()
        {

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
        //[Required]
        //public int Score { get; set; }

       // public string? Comments { get; set; }

        public int asTarget { get; set; }

        public int AgentSatisfaction { get; set; }

        public int asPercentage { get; set; }

        public int vsTarget { get; set; }

        public int visitSatisfaction { get; set; }

        public int vsPercentage { get; set; }

        public int QuizTarget { get; set; }

        public int QuizOnline { get; set; }

        public int QuizPercentage { get; set; }

        public int RamTarget { get; set; }

        public int FatalError { get; set; }

        public int RamPercentage { get; set; }


        public int MSTarget { get; set; }

        public int MysteryShopping {  get; set; }

        public int MSPercentage { get; set; }

        public int ResponsesCount { get; set; }

    }
}