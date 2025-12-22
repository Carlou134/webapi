using MediatR;
using webapi.Models;

namespace webapi.Features.Usuarios.Commands.CreateUsuario
{
    public record CreateUsuarioCommand(
        string Nombre,
        string Email,
        string Password 
    ):IRequest<OperationResult<bool>>;
}
