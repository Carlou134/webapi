using MediatR;
using webapi.Context;
using webapi.Models;

namespace webapi.Features.Usuarios.Commands.UpdateUsuario
{
    public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, OperationResult<bool>>
    {
        private readonly UsersContext _usersContext;

        public UpdateUsuarioCommandHandler(UsersContext usersContext)
        {
            _usersContext = usersContext!;
        }

        public UsersContext Get_usersContext()
        {
            return _usersContext;
        }

        public async Task<OperationResult<bool>> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();

            try
            {
                var usuario = await _usersContext
                    .Usuario
                    .FindAsync(request.Id, cancellationToken)
                    .ConfigureAwait(false) 
                    ?? throw new ArgumentNullException("No se encontró el usuario");


                usuario.NombreUsuario = request.Nombre;
                usuario.Email = request.Email;

                if(request.RolId.HasValue && request.RolId != usuario.RolId)
                {
                    usuario.RolId = request.RolId!.Value;
                }

                if(!BCrypt.Net.BCrypt.Verify(request.Password, usuario.Password))
                {
                    usuario.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                }

                await _usersContext.SaveChangesAsync(cancellationToken);


                result.Success = true;
                result.Message = "Registro actualizado correctamente";
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
