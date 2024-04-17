using LibraryQuotes.Models.DataBase.Interfaces;
using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryQuotes.Services
{
    public class CreateTokenService : ICreateTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabase _database;

        public CreateTokenService(IConfiguration configuration, IDatabase database)
        {
            _configuration = configuration;
            _database = database;
        }

        public async Task<string> GenerateToken(UserDTO user)
        {
            var userData = await _database.users.FirstOrDefaultAsync(u => u.Email == user.Email);

            var claims = new[]
            {
                new Claim("UserId", userData.UserId.ToString()),
                new Claim(ClaimTypes.Name, userData.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));
            var credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                                claims: claims,
                                expires: DateTime.Now.AddMinutes(5),
                                signingCredentials: credencial
                                );

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
