using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DTOs;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _LoginService;

        public AuthController(ILoginService loginService)
        {
            _LoginService = loginService!;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var result = await _LoginService.Login(request).ConfigureAwait(false);

                if (result == null) return Unauthorized("Credenciales incorrectas");

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
