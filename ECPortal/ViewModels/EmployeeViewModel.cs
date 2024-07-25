namespace Pk.Com.Jazz.ECP.ViewModels
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EmployeeViewModel
    {
        // Properties from the Employee model
        public string DisplayName { get; set; }
        public string UserAdLogin { get; set; }
        public string EmployeeId { get; set; }
        public string Remarks { get; set; }
        public bool IsEnabled { get; set; }

        // Property for UserRole
        public string UserRole { get; set; }

        // Property for UserId
        public string AppUserId { get; set; }

        // Property to hold the list of roles
        public IEnumerable<SelectListItem> Roles { get; set; }
    }

}
