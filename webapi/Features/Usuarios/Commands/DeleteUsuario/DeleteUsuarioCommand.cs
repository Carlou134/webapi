using MediatR;
using webapi.Models;

namespace webapi.Features.Usuarios.Commands.DeleteUsuario
{
    public record DeleteUsuarioCommand(
        Guid Id
        ) : IRequest<OperationResult<bool>>;
}
