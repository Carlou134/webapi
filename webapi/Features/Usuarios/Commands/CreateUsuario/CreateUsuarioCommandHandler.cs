using MediatR;
using webapi.Context;
using webapi.Models;

namespace webapi.Features.Usuarios.Commands.CreateUsuario
{
    public class CreateUsuarioCommandHandler : IRequestHandler<CreateUsuarioCommand, OperationResult<bool>>
    {
        private readonly UsersContext _usersContext;

        public CreateUsuarioCommandHandler(UsersContext usersContext)
        {
            _usersContext = usersContext!;
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
