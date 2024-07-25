using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pk.Com.Jazz.ECP.Data
{
    public class UserProfileViewModel
    {
        public UserProfileViewModel()
        {
            ModifiedDate = DateTime.Now;
            EntryDate = DateTime.Now;
           
        }

        [Display(Name = "Employee Number", ShortName = "Employee")]
        public double EmployeeId { get; set; }

        [Display(Name = "Display Name", ShortName = "Name")]
        public string UserDisplayName { get; set; }

        [Display(Name = "User Login", ShortName = "AD")]
        public string UserAdLogin { get; set; }

        [Display(Name = "User Role", ShortName = "Role")]
        public string UserRole { get; set; }

        [Display(Name = "Profile Picture", ShortName = "dp")]
        public byte[] ProfilePicture { get; set; }

        [Display(Name = "Summary", ShortName = "Summary")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "-no summary-")]
        public string Summary { get; set; }

        [Display(Name = "Last Modified Date", ShortName = "Modified")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "Entry Date", ShortName = "Entry")]
        public DateTime EntryDate { get; set; }

        // Derived property for display purposes
        [Display(Name = "User", ShortName = "User")]
        public string DisplayName
        {
            get { return $"{UserDisplayName} ({UserAdLogin.Replace("@jazz.com.pk", "")})"; }
        }

        public static Dictionary<string, string> SearchTypes()
        {
            return new Dictionary<string, string>
            {
                { "UserRole", "Role" },
                { "EmployeeId", "Employee Id" },
                { "UserAdLogin", "Email" }
            };
        }
    }
}
