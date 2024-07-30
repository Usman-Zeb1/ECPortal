using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pk.Com.Jazz.ECP.Models
{
    public class Employee
    {
        public Employee()
        {
            ModifiedDate = DateTime.Now;
            EntryDate = DateTime.Now;
            IsEnabled = true;

        }

        [Key]
        public int EmployeeId { get; set; }

        [Display(Name = "Employee Number", ShortName = "Employee")]
        public int EmployeeNumber { get; set; }

        [Display(Name = "Employee Name", ShortName = "Name")]
        [MaxLength(50)]
        public string? EmployeeName { get; set; }

        [Display(Name = "User Login", ShortName = "AD")]
        [MaxLength(50)]
        public string? UserAdLogin { get; set; }

        [Display(Name = "Profile Picture", ShortName = "dp")]
        public byte[]? ProfilePicture { get; set; }

        [Display(Name = "Summary", ShortName = "Summary")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "-no summary-")]
        public string? Summary { get; set; }

        [Display(Name = "Is Active?", ShortName = "Active")]
        public bool IsEnabled { get; set; }

        [Display(Name = "Last Edit By", ShortName = "User")]
        [MaxLength(50)]
        public string? EditBy { get; set; }

        [Display(Name = "Date of Joining", ShortName = "DOJ")]
        [MaxLength(50)]
        public string? DOJ { get; set; }

        [Display(Name = "Last Modified Date", ShortName = "Modified")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "Entry Date", ShortName = "Entry")]
        public DateTime EntryDate { get; set; }

        [ForeignKey("AppUser")]

        public string? AppUserId { get; set; }


        [ForeignKey("AppUserId")]
        public virtual AppUser? AppUser { get; set; }

        [ForeignKey("ExperienceCenter")]
        public int? ECID { get; set; }

        [Display(Name = "Manager ID")]
        public int? ManagerID { get; set; }

        [Display(Name = "RCCH ID")]
        public int? RCCHID { get; set; }

        [Display(Name = "HOD ID")]
        public int? HODID { get; set; }

        [Display(Name = "TL ID")]
        public int? TLID { get; set; }


        [ForeignKey("ECID")]
        public virtual EC? ExperienceCenter { get; set; }

        // Navigation properties for related tables
        public ICollection<QualityScores>? QualityScores { get; set; }
        public ICollection<QuizScores>? QuizScores { get; set; }
        public ICollection<EmployeeCommission>? EmployeeCommissions { get; set; }
        public ICollection<EmployeePerformance>? EmployeePerformances { get; set; }
        public ICollection<EmployeeFeedback>? EmployeeFeedbacks { get; set; }
        public ICollection<EmployeeEDA>? EmployeeEDAs { get; set; }
        public ICollection<EmployeeRecognition>? EmployeeRecognitions { get; set; }
        public ICollection<EmployeeTargets>? EmployeeTargets { get; set; }
        public ICollection<EmployeeTrainings>? EmployeeTrainings { get; set; }
        public ICollection<EmployeeSales>? EmployeeSales { get; set; }

        [Display(Name = "User", ShortName = "User")]
        public string UserAdLoginShort
        {
            get { return UserAdLogin.Replace("@jazz.com.pk", ""); }
        }

        [Display(Name = "Last Edit By", ShortName = "User")]
        public string? EditByShort
        {
            get { return EditBy?.Replace("@jazz.com.pk", ""); }
        }

        [Display(Name = "User", ShortName = "User")]
        public string DisplayName
        {
            get { return $"{EmployeeName} ({UserAdLoginShort})"; }
        }

        // New properties

        [MaxLength(50)]
        public string? Title { get; set; }

        [MaxLength(50)]
        public string CNIC { get; set; }   

        [MaxLength(50)]
        public string? DateOfJoiningBC { get; set; }

        [MaxLength(50)]
        public string? DateOfLeaving { get; set; }

        [MaxLength(50)]
        public string? MobileNumber { get; set; }

        [MaxLength(50)]
        public string? DeviceIMIE { get; set; }

        [MaxLength(50)]
        public string? PosIds { get; set; }

        [MaxLength(50)]
        public string? PosName { get; set; }

        [MaxLength(50)]
        public string? SalesId { get; set; }

        [MaxLength(50)]
        public string? WaridSalesId { get; set; }

        [MaxLength(50)]
        public string? TabsId { get; set; }

        [MaxLength(50)]
        public string? MfsId { get; set; }

        [MaxLength(50)]
        public string? SiebelId { get; set; }

        [MaxLength(50)]
        public string? EficsId { get; set; }

        [MaxLength(50)]
        public string? EficsId2 { get; set; }

        [MaxLength(50)]
        public string? QmaticLogin { get; set; }

        [MaxLength(50)]
        public string? QmaticPowerLogin { get; set; }

        [MaxLength(50)]
        public string? EmailAddress { get; set; }
    }
}