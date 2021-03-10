using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SystemyWP.API.Pages.Account
{
    public class Client : BasePage
    {
        [BindProperty] public ModeratorRegisterForm Form { get; set; } 
        
        public class ModeratorRegisterForm
        {
            [Required] 
            public string ReturnUrl { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            public string Code { get; set; }
            [Required]
            public string Username { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            
            [Required]
            [DataType(DataType.Password)]
            [Compare(nameof(Password))]
            public string ConfirmPassword { get; set; }
        }
        
        public void OnGet(string returnUrl, string code, string email)
        {
            Form = new ModeratorRegisterForm
            {
                Email = email,
                Code = code,
                ReturnUrl = returnUrl
            };
        }

        public async Task<IActionResult> OnPostAsync(
            [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] SignInManager<IdentityUser> signInManager)
        {
            if (!ModelState.IsValid)
            {
                return Page(); 
            }
            
            var existingUser = await userManager.FindByNameAsync(Form.Username);
            if (existingUser is not null)
            {
                CustomErrors.Add("Ta nazwa użytkownika jest już zajęta.");
                return Page();
            }
            
            var user = await userManager.FindByEmailAsync(Form.Email);

            var resetPassword = await userManager
                .ResetPasswordAsync(user, Form.Code, Form.Password);

            if (resetPassword.Succeeded)
            {
                user.UserName = Form.Username;
                user.EmailConfirmed = true;
         
                userManager.UpdateAsync(user);
                
                await userManager.RemoveFromRoleAsync(user, SystemyWPConstants.Roles.Invited);
                await userManager.AddToRoleAsync(user, SystemyWPConstants.Roles.Client);   
                
                await signInManager.SignInAsync(user, true);
                return Redirect(Form.ReturnUrl);
            }
            
            CustomErrors.Add("Wystąpił błąd, spróbuj jeszcze raz.");
            
            return Page();
        }
    }
}