using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SystemyWP.API.Pages.Account
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
            [FromServices] SignInManager<IdentityUser> signInManager)
        {
            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            var signInResult = await signInManager
                .PasswordSignInAsync(Form.Username, Form.Password, true, false);
            
            if (signInResult.Succeeded)
            {
                return Redirect(Form.ReturnUrl);
            }
            
            CustomErrors.Add("Invalid Login Attempt");

            return Page();
        }
        
        public void OnGet(string returnUrl)
        {
            Form = new LoginForm {ReturnUrl = returnUrl};
        }
    }
}