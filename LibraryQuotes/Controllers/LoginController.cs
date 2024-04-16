using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryQuotes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;

        public LoginController(ILoginService loginService, IConfiguration configuration)
        {
            _loginService = loginService;
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            var user = await _loginService.GetUser(userDTO);

            if (user is null)
            {
                return BadRequest(new { message = "Credenciales invalidas" });
            }

            string jwtToken = GenerateToken(userDTO);

            return Ok(new { token = jwtToken });
        }

        private string GenerateToken(UserDTO user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
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
