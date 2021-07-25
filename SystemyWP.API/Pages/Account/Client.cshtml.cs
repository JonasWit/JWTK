using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SystemyWP.API.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SystemyWP.API.Pages.Account
{
    public class Client : BasePage
    {
        [BindProperty] public RegisterForm Form { get; set; }
        public string PortalPrivacyUrl => $@"{_settings.PortalUrl}\portal-web\privacy-policy";
        public string PortalTermsOfServiceUrl => $@"{_settings.PortalUrl}\portal-web\terms-of-service";

        private readonly CorsSettings _settings;

        public Client(IOptionsMonitor<CorsSettings> optionsMonitor)
        {
            _settings = optionsMonitor.CurrentValue;
        }
        
        public class RegisterForm
        {
            [Required] 
            public string ReturnUrl { get; set; }
            
            [Required(ErrorMessage = "Pole jest wymagane")]
            [DataType(DataType.EmailAddress, ErrorMessage = "Niepoprawny adres email")]
            public string Email { get; set; }
            
            public string Code { get; set; }
            
            [Required(ErrorMessage = "Pole jest wymagane")]
            [StringLength(20, ErrorMessage = "Nazwa użytkownika może mieć maksymalnie 20 znaków")]
            public string Username { get; set; }
            
            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Pole jest wymagane")]
            [StringLength(25, ErrorMessage = "Hasło musi mieć od 16 do 25 znaków", MinimumLength = 16)]
            [RegularExpression(SystemyWpConstants.Patterns.PasswordPattern, 
                ErrorMessage = "Hasło musi zawierać małą i duża literę, cyfrę i znak specjalny")]
            public string Password { get; set; }
            
            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Pole jest wymagane")]
            [Compare(nameof(Password), ErrorMessage = "Hasła nie są identyczne")]
            public string ConfirmPassword { get; set; }
            
            [RegularExpression("(True|true)", ErrorMessage = "Akceptacja polityki prywatności i regulaminu jest wymagana!")]
            public bool RulesAccepted { get; set; }
            
            [RegularExpression("(True|true)", ErrorMessage = "Akceptacja faktur w formie elektronicznej jest wymagana!")]
            public bool InvoiceAccepted { get; set; }
        }
        
        public void OnGet(string returnUrl, string code, string email)
        {
            Form = new RegisterForm
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
            if (!ModelState.IsValid) return Page();

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
                
                await userManager.RemoveClaimAsync(user, SystemyWpConstants.Claims.InvitedClaim);
                await userManager.AddClaimAsync(user, SystemyWpConstants.Claims.UserClaim); 
                
                await signInManager.SignInAsync(user, true);
                return Redirect(Form.ReturnUrl);
            }
            
            CustomErrors.Add("Wystąpił błąd, spróbuj jeszcze raz.");
            return Page();
        }
    }
}