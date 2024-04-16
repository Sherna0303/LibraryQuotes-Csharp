using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryQuotes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;
        private readonly ICreateToken _createToken;

        public AuthController(ILoginService loginService, IRegisterService registerService ,ICreateToken createToken)
        {
            _loginService = loginService;
            _registerService = registerService;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            var user = await _registerService.RegisterUser(userRegisterDTO);

            if (user is null)
            {
                return BadRequest(new { message = "Error" });
            }

            string jwtToken = _createToken.GenerateToken(new UserDTO() { Email = user.Email, Password = user.Password});

            return Ok(new { token = jwtToken });
        }
    }
}
