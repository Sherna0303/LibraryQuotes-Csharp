using LibraryQuotes.Models.DTOS.User;
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
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ICreateToken _createToken;

        public AuthController(ILoginService loginService, ICreateToken createToken)
        {
            _loginService = loginService;
            _createToken = createToken;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            var user = await _loginService.GetUser(userDTO);

            if (user is null)
            {
                return BadRequest(new { message = "Credenciales invalidas" });
            }

            string jwtToken = _createToken.GenerateToken(userDTO);

            return Ok(new { token = jwtToken });
        }

        
    }
}
