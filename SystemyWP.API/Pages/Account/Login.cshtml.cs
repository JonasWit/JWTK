using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SystemyWP.API.Services.Logging;
using SystemyWP.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Pages.Account
{
    public class Login : BasePage
    {
        [BindProperty] public LoginForm Form { get; set; }

        public class LoginForm
        {
            [Required] public string ReturnUrl { get; set; }
            [Required] public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(
            [FromServices] PortalLogger portalLogger,
            [FromServices] SignInManager<IdentityUser> signInManager,
            [FromServices] UserManager<IdentityUser> userManager,
            [FromServices] IHostEnvironment env)
        {
            try
            {
                if (string.IsNullOrEmpty(Form.ReturnUrl)) Form.ReturnUrl = env.IsDevelopment() ? @"https://localhost:3000/" : @"https://portal.systemywp.pl";
                if (!ModelState.IsValid) return Page();

                var signInResult = await signInManager
                    .PasswordSignInAsync(Form.Username, Form.Password, true, lockoutOnFailure: true);
                
                if (signInResult.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(Form.Username);
                    await portalLogger.Log(HttpContext.Request.Path.Value, user, LogType.Access, "Zalogowano",
                        "Logging Page");
                    return Redirect(Form.ReturnUrl);
                }

                if (signInResult.IsLockedOut)
                {
                    var user = await userManager.FindByNameAsync(Form.Username);
                    await portalLogger.Log(HttpContext.Request.Path.Value, user, LogType.Access, "Nieudana próba logowania - konto zablokowane",
                        "Logging Page");
                    return RedirectToPage("./Lockout");
                }

                if (!signInResult.Succeeded)
                {
                    await portalLogger
                        .Log(new PortalLogRecord
                        {
                            Endpoint = HttpContext.Request.Path.Value,
                            LogType = LogType.Access,
                            Description = "Nieudana próba logowania",
                            CreatedBy = "Logging Page",
                            UserEmail = "",
                            UserName = Form.Username,
                            UserId = "",
                        });         
                }
                
                CustomErrors.Add("Nieudana próba logowania!");
                return Page();
            }
            catch (Exception e)
            {
                await portalLogger
                    .Log(new PortalLogRecord
                    {
                        LogType = LogType.Exception,
                        Description = "Login Attempt",
                        CreatedBy = "system",
                        UserEmail = "",
                        UserName = Form.Username,
                        ExceptionMessage = e.Message,
                        ExceptionStackTrace = e.StackTrace
                    });
                return Page();
            }
        }

        public void OnGet(string returnUrl)
        {
            Form = new LoginForm {ReturnUrl = returnUrl};
        }
    }
}