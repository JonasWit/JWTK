using Domain.MasterServiceShared.DTOs;

namespace Domain.Shared.MasterServiceShared.DTOs;

public class UserPasswordResetForm : UserPasswordForm
{
    public string? Token { get; set; }
}