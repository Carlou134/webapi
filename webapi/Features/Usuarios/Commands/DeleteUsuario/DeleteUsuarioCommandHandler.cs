using MediatR;
using System.Runtime.InteropServices;
using webapi.Context;
using webapi.Models;

namespace webapi.Features.Usuarios.Commands.DeleteUsuario
{
    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, OperationResult<bool>>
    {
        private readonly UsersContext _context;

        public DeleteUsuarioCommandHandler(UsersContext context)
        {
            _context = context!;
        }

        public async Task<OperationResult<bool>> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();

            try
            {
                var user = await _context
                    .Usuario
                    .FindAsync(request.Id, cancellationToken)
                    .ConfigureAwait(false) ?? throw new ArgumentNullException("No se encontró el usuario");

                _context.Remove(user);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                result.Success = true;
                result.Message = "OK";
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
