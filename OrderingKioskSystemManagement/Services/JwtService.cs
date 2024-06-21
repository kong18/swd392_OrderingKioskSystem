using IdentityModel;
using OrderingKioskSystem.Application.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderingKioskSystemManagement.Api.Services
{
    public class JwtService : IJwtService
    {
        public string CreateToken(string entityId, string role, string email)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, entityId),
                new(JwtClaimTypes.Email, email),
                new(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("from sonhohuu deptrai6mui with love"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       
        public string CreateToken(string email, string roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Email, email),
                new(ClaimTypes.Role, roles)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("from sonhohuu deptrai6mui with love"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateToken(string Id)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("from sonhohuu deptrai6mui with love"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                expires: DateTime.Now.AddDays(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateToken(string subject, string role, int expiryDays)
        {
            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub, subject),
                new Claim(ClaimTypes.Role, role),
              
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("from sonhohuu deptrai6mui with love"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(expiryDays),
                claims: claims,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
