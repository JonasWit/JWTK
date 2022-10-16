using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using VappsWeb.Services;

namespace VappsWeb.Pages
{
    public partial class Login
    {
        private readonly LoginForm _model = new();

        [Inject]
        public AuthService AuthService { get; set; } = null!;

        [Inject]
        public HttpClient HttpClient { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public NavigationManager Navigation { get; set; } = null!;

        public class LoginForm
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required]
            public string Password { get; set; } = string.Empty;
        }

        private async Task OnValidSubmit(EditContext context)
        {
            if (await AuthService.SignIn(_model.Email, _model.Password))
            {
                _ = Snackbar.Add("Logged in!", Severity.Success);
                Navigation.NavigateTo("/");
                return;
            }

            _ = Snackbar.Add("Sign in failed! Please try again", Severity.Error);
            StateHasChanged();
        }
    }
}