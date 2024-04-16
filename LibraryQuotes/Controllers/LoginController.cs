using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryQuotes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            var user = await _loginService.GetUser(userDTO);

            if (user is null)
            {
                return BadRequest(new { message = "Credenciales invalidas" });
            }

            return Ok(new { token = "Token" });
        }
    }
}
