using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pk.Com.Jazz.ECP.Models;
using Pk.Com.Jazz.ECP.Data;

namespace Pk.Com.Jazz.ECP.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ECContext _context;

        public Employee Employee { get; set; }

        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ECContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Name")]
            public string Name { get; set; }

            [Display(Name = "Summary")]
            public string Summary { get; set; }

            [Display(Name = "Profile Picture")]
            public IFormFile? ProfilePicture { get; set; } // Profile picture property
        }

        private async Task LoadAsync(AppUser user)
        {
            // Fetch phone number asynchronously
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            // Fetch employee record asynchronously
            Employee = await _context.Employee.FirstOrDefaultAsync(e => e.AppUserId == user.Id);

            // Initialize Username
            Username = Employee?.UserAdLoginShort ?? string.Empty;

            // Initialize InputModel with default values
            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Name = Employee?.EmployeeName,
                Summary = Employee?.Summary
            };

            // Check if Employee is not null and process profile picture
            if (Employee?.ProfilePicture != null)
            {
                using (var ms = new MemoryStream(Employee.ProfilePicture))
                {
                    Input.ProfilePicture = new FormFile(ms, 0, ms.Length, null, "profile_picture.jpg")
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "image/jpeg"
                    };
                }
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            // Update phone number if changed
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            // Fetch employee record again for update
            Employee = await _context.Employee.FirstOrDefaultAsync(e => e.AppUserId == user.Id);
            if (Employee != null)
            {
                // Update employee properties
                Employee.Summary = Input.Summary ?? Employee.Summary; // Only update if new value is provided
                Employee.EmployeeName = Input.Name ?? Employee.EmployeeName;

                // Handle profile picture upload
                if (Input.ProfilePicture != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Input.ProfilePicture.CopyToAsync(memoryStream);
                        Employee.ProfilePicture = memoryStream.ToArray();
                    }
                }

                // Save changes to the database
                _context.Employee.Update(Employee);
                await _context.SaveChangesAsync();
            }

            // Refresh sign-in to ensure the updated information is used
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
