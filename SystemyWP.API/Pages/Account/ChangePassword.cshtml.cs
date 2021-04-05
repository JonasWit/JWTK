﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SystemyWP.API.Pages.Account
{
    [Authorize]
    public class ChangePassword : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ChangePassword(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty] public ResetPasswordForm Form { get; set; }

        [TempData] public string StatusMessage { get; set; }

        public class ResetPasswordForm
        {
            [Required]
            public string ReturnUrl { get; set; }
            
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Obecne Hasło")]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = "Hasło jest wymagane!")]
            [DataType(DataType.Password)]
            [StringLength(25, ErrorMessage = "Hasło musi mieć od 12 do 25 znaków", MinimumLength = 12)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{12,}$", 
                ErrorMessage = "Hasło musi zawierać małą i duża literę, cyfrę i znak specjalny")]
            [Display(Name = "Hasło")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Powtórz nowe Hasło")]
            [Compare("NewPassword", ErrorMessage = "Hasła nie są identyczne!")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Form = new ResetPasswordForm {ReturnUrl = returnUrl};
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult =
                await _userManager.ChangePasswordAsync(user, Form.OldPassword, Form.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage("./Login",new {returnUrl = Form.ReturnUrl});
        }

        public IActionResult OnPostReturn()
        {
            if (string.IsNullOrEmpty(Form.ReturnUrl))
            {
                
            }
            
            return Redirect(Form.ReturnUrl);
        } 
    }
}