using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using src.Contracts;
using src.Models.UserAccount;

namespace src.Services
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration config;
        public JwtGenerator(IConfiguration config, UserManager<User> userManager)
        {
            this.config = config;
            this.userManager = userManager;
        }

        public async Task<string> GenerateJwtTokenString(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Firstname)
            };

            // include user role(s) inside the token
            var roles = await userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddSeconds(5),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return await Task.FromResult(tokenHandler.WriteToken(token));
        }

        public async Task<string> GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return await Task.Run(() => Convert.ToBase64String(randomNumber));
            }
        }
    }
}
