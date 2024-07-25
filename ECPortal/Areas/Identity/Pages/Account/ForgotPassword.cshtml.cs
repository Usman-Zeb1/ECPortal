using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

using Pk.Com.Jazz.ECP.Data;
using Pk.Com.Jazz.ECP.Models;

namespace Pk.Com.Jazz.ECP.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ECContext _context;
        public ForgotPasswordModel(UserManager<AppUser> userManager, IEmailSender emailSender, ECContext context)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
                
                var code = await _userManager.GeneratePasswordResetTokenAsync(user); // For more information on how to enable account confirmation and password reset please  // visit https://go.microsoft.com/fwlink/?LinkID=532713
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(Input.Email, "Reset Password - Experience Center Portal (ECP)", EmailBody(callbackUrl));

                //save tokens in AppUserTokens Table
                //--------------------------------------------
                var appUserToken = new AppUserToken();
                appUserToken.UserAdLogin = user.Email;
                appUserToken.Token = callbackUrl;
                appUserToken.TokenType = "Forgot Password";

                _context.Add(appUserToken);
                await _context.SaveChangesAsync();
                //--------------------------------------------

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }

        private string EmailBody(string callBackUrl) {

            string emailBody = "<p>Dear User,<br />" 
                              + "We have received your password reset request against Experience Center web portal. Kindly use below link. Thank you.<p>"
                              + $"<p>Reset Password by <a href='{HtmlEncoder.Default.Encode(callBackUrl)}'>clicking here</a>.</p>"
                              + "<p>Regards,<br />"
                              + "CE - Development Team</p>";

            return emailBody;
        }
    }
}
