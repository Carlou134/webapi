using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Features.Usuarios.Commands.CreateUsuario;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService!;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("listar")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            try
            {
                return Ok(await _userService.GetUsuarios());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] CreateUsuarioCommand request)
        {
            try
            {
                CancellationTokenSource cancellationToken = new();
                var result = await _userService.Save(request, cancellationToken.Token).ConfigureAwait(false);
                return (result.Success) ? Ok(result.Message) : BadRequest(result.Errors);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
