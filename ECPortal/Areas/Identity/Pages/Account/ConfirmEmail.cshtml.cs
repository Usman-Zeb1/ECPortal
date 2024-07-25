using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;

namespace Pk.Com.Jazz.ECP.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ECContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ConfirmEmailModel(UserManager<AppUser> userManager, ECContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                return Page();
            }

            // Add the user to the "Agent" role
            var roleName = "Agent";
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                // Create the role if it doesn't exist
                var role = new IdentityRole(roleName);
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, roleName);

            // Creating employee record
            var employee = new Employee
            {
                AppUserId = user.Id,
                UserAdLogin = user.Email,
                EmployeeNumber = 12345,
                EmployeeName = user.Email,
                DOJ = "N/A",
                EditBy = "N/A"

                // Add other properties you want to save for the employee
            };

            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            StatusMessage = "Thank you for confirming your email.";
            return Page();
        }

    }
}
