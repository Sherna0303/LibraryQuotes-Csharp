using FluentValidation;
using LibraryQuotes.Models.DTOS.Quoation;
using LibraryQuotes.Models.DTOS.User;
using LibraryQuotes.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryQuotes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IRegisterService _registerService;
        private readonly ICreateTokenService _createToken;
        private readonly IValidator<UserDTO> _userValidator;
        private readonly IValidator<UserRegisterDTO> _userRegisterValidator;

        public AuthController(ILoginService loginService, IRegisterService registerService ,ICreateTokenService createToken, IValidator<UserDTO> userValidator, IValidator<UserRegisterDTO> userRegisterValidator)
        {
            _loginService = loginService;
            _registerService = registerService;
            _createToken = createToken;
            _userValidator = userValidator;
            _userRegisterValidator = userRegisterValidator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            var validateUser = await _userValidator.ValidateAsync(userDTO);

            if (!validateUser.IsValid)
            {
                return BadRequest(validateUser.Errors);
            }

            var user = await _loginService.GetUser(userDTO);

            if (user is null)
            {
                return BadRequest(new { message = "Credenciales invalidas" });
            }

            string jwtToken = await _createToken.GenerateToken(userDTO);

            return Ok(new { token = jwtToken });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
        {
            var validateUserRegister = await _userRegisterValidator.ValidateAsync(userRegisterDTO);

            if (!validateUserRegister.IsValid)
            {
                return BadRequest(validateUserRegister.Errors);
            }

            var user = await _registerService.RegisterUser(userRegisterDTO);

            if (user is null)
            {
                return BadRequest(new { message = "Error" });
            }

            string jwtToken = await _createToken.GenerateToken(new UserDTO() { Email = user.Email, Password = user.Password});

            return Ok(new { token = jwtToken });
        }

        
        [HttpGet("verifyToken")]
        [Authorize]
        public IActionResult VerifyToken()
        {
            return Ok(new { verify = "OK" });
        }
    }
}
