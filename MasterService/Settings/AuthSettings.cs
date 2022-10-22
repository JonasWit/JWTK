namespace MasterService.API.Settings;

public class AuthSettings
{
    public string Audience { get; set; } 
    public string Issuer { get; set; } 
    public string SecretKey { get; set; } 
    public string Salt { get; set; } 
}