using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Pk.Com.Jazz.ECP.Utilities
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            // Ensure that the required roles exist
            string[] roleNames = { "ECM", "Admin", "Agent", "RCCH", "TeamLead", "HOD", "OPG" };
            foreach (string roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    // Create the role if it doesn't exist
                    IdentityRole role = new IdentityRole(roleName);
                    role.NormalizedName = roleName.ToUpper();
                    role.ConcurrencyStamp = Guid.NewGuid().ToString();
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
