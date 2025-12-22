using MediatR;
using webapi.Context;
using webapi.Models;
using webapi.Notifications.Usuario.UsuarioCreado;

namespace webapi.Features.Usuarios.Commands.CreateUsuario
{
    public class CreateUsuarioCommandHandler : IRequestHandler<CreateUsuarioCommand, OperationResult<bool>>
    {
        private readonly UsersContext _usersContext;
        private readonly IMediator _mediator;

        public CreateUsuarioCommandHandler(UsersContext usersContext, IMediator mediator)
        {
            _usersContext = usersContext!;
            _mediator = mediator!;
        }

        public async Task<OperationResult<bool>> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();

            try
            {
                var usuario = new Usuario
                {
                    UsuarioId = Guid.NewGuid(),
                    RolId = Guid.Parse("240ACF13-8D8C-433D-90E0-F9C8B1DEA11C"),
                    NombreUsuario = request.Nombre,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
                };

                await _usersContext.AddAsync(usuario, cancellationToken).ConfigureAwait(false);
                await _usersContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                await _mediator.Publish(new UsuarioCreadoNotification(usuario.UsuarioId), cancellationToken);

                result.Success = true;
                result.Message = "Usuario registrado con éxito!";
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = "Error";
                result.Errors.Add(ex.Message);
            }

            return result;
        }
    }
}
