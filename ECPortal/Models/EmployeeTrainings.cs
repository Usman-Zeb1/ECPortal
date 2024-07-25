using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class EmployeeTrainings
    {

        public EmployeeTrainings() {

            ModifiedDate = DateTime.Now;
            InsertDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public DateTime TrainingDate { get; set; }

        [Required]
        public string TrainingName { get; set; }

        public DateTime? CompletionDate { get; set; }

        [Required]
        [Display(Name = "Inserted Date")]
        public DateTime InsertDate { get; set; }

        [Required]
        [Display(Name = "Last Modified Date")]
        public DateTime ModifiedDate { get; set; }

        public string Status { get; set; }

        // Additional columns deemed necessary
        public string? Comments { get; set; }
    }
}
