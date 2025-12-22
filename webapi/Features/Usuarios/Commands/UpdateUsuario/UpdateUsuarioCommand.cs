using MediatR;
using webapi.Models;

namespace webapi.Features.Usuarios.Commands.UpdateUsuario
{
    public record UpdateUsuarioCommand(
        Guid Id,
        Guid? RolId,
        string Nombre,
        string Email,
        string Password
        ):IRequest<OperationResult<bool>>;
}
