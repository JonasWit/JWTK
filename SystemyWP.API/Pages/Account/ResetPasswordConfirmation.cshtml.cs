﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SystemyWP.API.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordConfirmation : PageModel
    {
        public void OnGet()
        {
            
        }
    }
}