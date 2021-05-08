using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Systemywp.Api.Pages.Account
{
    public class Client : BasePage
    {
        [BindProperty] public ModeratorRegisterForm Form { get; set; } 
        
        public class ModeratorRegisterForm
        {
            [Required] 
            public string ReturnUrl { get; set; }
            [Required]
            [DataType(DataType.EmailAddress, ErrorMessage = "Niepoprawny adres email")]
            public string Email { get; set; }
            public string Code { get; set; }
            [Required]
            [StringLength(20, ErrorMessage = "Nazwa użytkownika może mieć maksymalnie 20 znaków")]
            [RegularExpression(@"[A-Za-z0-9._-]", 
                ErrorMessage = "Niepoprawna nazwa użytkownika")]
            public string Username { get; set; }
            [Required]
            [StringLength(25, ErrorMessage = "Hasło musi mieć od 12 do 25 znaków", MinimumLength = 12)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{12,}$", 
                ErrorMessage = "Hasło musi zawierać małą i duża literę, cyfrę i znak specjalny")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            
            [Required]
            [DataType(DataType.Password)]
            [Compare(nameof(Password), ErrorMessage = "Hasła nie są identyczne")]
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
                
                await userManager.UpdateAsync(user);
                
                await userManager.RemoveFromRoleAsync(user, SystemyWpConstants.Roles.Invited);
                await userManager.AddToRoleAsync(user, SystemyWpConstants.Roles.Client);   
                
                await signInManager.SignInAsync(user, true);
                return Redirect(Form.ReturnUrl);
            }
            
            CustomErrors.Add("Wystąpił błąd, spróbuj jeszcze raz.");
            
            return Page();
        }
    }
}