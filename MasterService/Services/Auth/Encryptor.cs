using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using MasterService.API.Settings;

namespace MasterService.API.Services.Auth;

public class Encryptor
{
    private readonly IOptionsMonitor<AuthSettings> _optionsMonitor;

    public Encryptor(IOptionsMonitor<AuthSettings> optionsMonitor)
    {
        _optionsMonitor = optionsMonitor;
    }

    public string Encrypt(string password)
    {
        var bytes = SHA512
            .Create()
            .ComputeHash(Encoding.UTF8.GetBytes(_optionsMonitor.CurrentValue.Salt + password));
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }
}