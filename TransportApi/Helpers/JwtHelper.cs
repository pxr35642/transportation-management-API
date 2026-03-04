using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TransportApi.Models;

namespace TransportApi.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _config;

        public JwtHelper(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>依據使用者資料產生 JWT Token</summary>
        public string GenerateToken(User user)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var secretKey   = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)
            );

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,           user.Username!),
                new Claim(ClaimTypes.Role,           user.Role!),
                new Claim("FullName",                user.FullName!),
            };

            var token = new JwtSecurityToken(
                issuer:             jwtSettings["Issuer"],
                audience:           jwtSettings["Audience"],
                claims:             claims,
                expires:            DateTime.UtcNow.AddMinutes(
                                        double.Parse(jwtSettings["ExpireMinutes"]!)
                                    ),
                signingCredentials: new SigningCredentials(
                                        secretKey, SecurityAlgorithms.HmacSha256
                                    )
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}