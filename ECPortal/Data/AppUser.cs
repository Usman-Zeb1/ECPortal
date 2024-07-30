using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pk.Com.Jazz.ECP.Data
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            ModifiedDate = DateTime.Now;
            EntryDate = DateTime.Now;
        }

        // Any other properties specific to AppUser can go here

        [Display(Name = "Last Modified Date", ShortName = "Modified")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "Entry Date", ShortName = "Entry")]
        public DateTime EntryDate { get; set; }

<<<<<<< Updated upstream
        public bool isEnabled {  get; set; }
=======
        public bool isEnabled { get; set; }
>>>>>>> Stashed changes

        public static Dictionary<string, string> Types()
        {
            throw new NotImplementedException();
        }

        public static Dictionary<string, string> SearchTypes()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            keyValuePairs.Add("UserRole", "Role");
            keyValuePairs.Add("EmployeeId", "Employee Id");
            keyValuePairs.Add("UserAdLogin", "Email");
            return keyValuePairs;
        }
    }
}