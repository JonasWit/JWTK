using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using SystemyWP.API.Services.Email;
using SystemyWP.API.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace SystemyWP.API.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPassword : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ForgotPassword(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync([FromServices] EmailClient emailSender)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                await emailSender.SendEmailAsync(
                    Input.Email,
                    "Systemywp.pl - Reset Hasła", 
                    EmailTemplates.PasswordResetEmailBody(callbackUrl));
                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            
            return Page();
        }
    }
}