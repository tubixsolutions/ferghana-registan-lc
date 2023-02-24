using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RegistanFerghanaLC.Domain.Entities;
using RegistanFerghanaLC.Service.Interfaces.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RegistanFerghanaLC.Service.Services.Common;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config )
    {
        this._config = config.GetSection("Jwt");
    }
    public string GenerateToken(Human user, string role)
    {
        var claims = new[]
           {
                new Claim("Id", user.Id.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("ImagePath", (user.Image is null)?"":user.Image),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Role, $"{role}")
            };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new JwtSecurityToken(_config["Issuer"], _config["Audience"], claims,
            expires: DateTime.Now.AddMinutes(double.Parse(_config["Lifetime"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
