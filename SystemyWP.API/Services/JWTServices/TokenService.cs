using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using SystemyWP.API.Data.Models;
using SystemyWP.API.Settings;

namespace SystemyWP.API.Services.JWTServices;

public class TokenService
{
    private readonly IOptionsMonitor<AuthSettings> _optionsMonitor;

    public TokenService(IOptionsMonitor<AuthSettings> optionsMonitor) => _optionsMonitor = optionsMonitor;

    public bool ValidateToken(string token)
    {
        var secretBytes = Encoding.UTF8.GetBytes(_optionsMonitor.CurrentValue.SecretKey);
        var key = new SymmetricSecurityKey(secretBytes);
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            _ = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _optionsMonitor.CurrentValue.Issuer,
                ValidAudience = _optionsMonitor.CurrentValue.Audience,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero,
            }, out SecurityToken validatedToken);
        }
        catch
        {
            return false;
        }
        return true;
    }

    public string GenerateJwtToken(User user)
    {
        Claim[] claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Claims.First(c => c.ClaimType == ClaimTypes.Email).ClaimValue),
            new Claim(ClaimTypes.Name, user.Claims.First(c => c.ClaimType == ClaimTypes.Name).ClaimValue),
            new Claim(ClaimTypes.Role, user.Claims.First(c => c.ClaimType == ClaimTypes.Role).ClaimValue),
            new Claim(ClaimTypes.NameIdentifier, user.Claims.First(c => c.ClaimType == ClaimTypes.NameIdentifier).ClaimValue),
        };

        var secretBytes = Encoding.UTF8.GetBytes(_optionsMonitor.CurrentValue.SecretKey);
        var key = new SymmetricSecurityKey(secretBytes);

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            _optionsMonitor.CurrentValue.Issuer,
            _optionsMonitor.CurrentValue.Audience,
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(14),
            signingCredentials);

        var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenJson;
    }

    public string GeneratePasswordResetJwtToken(User user)
    {
        Claim[] claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Claims.First(c => c.ClaimType == ClaimTypes.Email).ClaimValue),
        };

        var secretBytes = Encoding.UTF8.GetBytes(_optionsMonitor.CurrentValue.SecretKey);
        var key = new SymmetricSecurityKey(secretBytes);

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            _optionsMonitor.CurrentValue.Issuer,
            _optionsMonitor.CurrentValue.Audience,
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(2),
            signingCredentials);

        var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenJson;
    }

    public string GenerateEmailConfirmationToken(string email)
    {
        Claim[] claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
        };

        var secretBytes = Encoding.UTF8.GetBytes(_optionsMonitor.CurrentValue.SecretKey);
        var key = new SymmetricSecurityKey(secretBytes);

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            _optionsMonitor.CurrentValue.Issuer,
            _optionsMonitor.CurrentValue.Audience,
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMonths(1),
            signingCredentials);

        var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenJson;
    }

    public string GetTokenClaim(string token, string claimName)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            return securityToken.Claims.FirstOrDefault(c => c.Type.Equals(claimName))?.Value;
        }
        catch (Exception)
        {
            return null;
        }
    }
}