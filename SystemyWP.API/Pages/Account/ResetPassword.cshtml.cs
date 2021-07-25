using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace SystemyWP.API.Pages.Account
{
    [AllowAnonymous]
    public class ResetPassword : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ResetPassword(UserManager<IdentityUser> userManager) => _userManager = userManager;

        [BindProperty] public InputModel Input { get; set; }

        public class InputModel
        {
            [Required] [EmailAddress] public string Email { get; set; }

            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Hasło jest wymagane!")]
            [StringLength(25, ErrorMessage = "Hasło musi mieć od 16 do 25 znaków", MinimumLength = 16)]
            [RegularExpression(SystemyWpConstants.Patterns.PasswordPattern, 
                ErrorMessage = "Hasło musi zawierać małą i duża literę, cyfrę i znak specjalny")]
            [Display(Name = "Hasło")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }

            Input = new InputModel
            {
                Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}