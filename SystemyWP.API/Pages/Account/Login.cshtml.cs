using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Systemywp.Api.Pages.Account
{
    public class Login : BasePage
    {
        [BindProperty] public LoginForm Form { get; set; } 
        
        public class LoginForm
        {
            [Required]
            public string ReturnUrl { get; set; }
            [Required]
            public string Username { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] IHostEnvironment env)
        {
            if (string.IsNullOrEmpty(Form.ReturnUrl))
            {
                Form.ReturnUrl = env.IsDevelopment() ? 
                    @"https://localhost:3000/" : 
                    @"https://portal.systemywp.pl";
            }
 
            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            var signInResult = await signInManager
                .PasswordSignInAsync(Form.Username, Form.Password, true, lockoutOnFailure: true);

            if (signInResult.Succeeded)
            {
                return Redirect(Form.ReturnUrl);
            }
            if (signInResult.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            
            CustomErrors.Add("Nieudana próba logowania!");

            return Page();
        }
        
        public void OnGet(string returnUrl)
        {
            Form = new LoginForm {ReturnUrl = returnUrl};
        }
    }
}