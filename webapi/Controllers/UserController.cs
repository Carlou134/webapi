using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapi.Features.Usuarios.Commands.CreateUsuario;
using webapi.Features.Usuarios.Commands.UpdateUsuario;
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
                return Ok(await _userService.GetUsuarios().ConfigureAwait(false));
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
                return (result.Success) ? Ok(result) : BadRequest(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN,USER")]
        [HttpPut("actualizar")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] UpdateUsuarioCommand request)
        {
            try
            {
                CancellationTokenSource cancellationToken = new();

                if (request.RolId.HasValue)
                {
                    if (GetCurrentRol() != "ADMIN")
                    {
                        return Unauthorized("No tiene permitido tener rol de administrador");
                    }
                }
                
                if ((GetCurrentRol() == "ADMIN" || GetCurrentUserId() == request.Id))
                {
                    return Ok(await _userService.Update(request, cancellationToken.Token));
                }

                return Unauthorized("No tiene acceso a esta función");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        protected Guid GetCurrentUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        protected string GetCurrentRol()
        {
            return User.FindFirst(ClaimTypes.Role)!.Value;
        }
    }
}
