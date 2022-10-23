namespace Domain.MasterServiceShared.DTOs;

public class UserChangePasswordForm
{
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
}