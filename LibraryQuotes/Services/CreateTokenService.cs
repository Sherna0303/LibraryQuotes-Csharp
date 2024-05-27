using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Repository.User;
using LibraryQuotes.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryQuotes.Services
{
    public class CreateTokenService : ICreateTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public CreateTokenService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<string> GenerateToken(UserDTO user)
        {
            var userData = await _userRepository.GetByEmail(user.Email);

            var claims = new[]
            {
                new Claim("UserId", userData.UserId.ToString()),
                new Claim(ClaimTypes.Name, userData.Name),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));
            var credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                                claims: claims,
                                expires: DateTime.Now.AddMinutes(60),
                                signingCredentials: credencial
                                );

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
