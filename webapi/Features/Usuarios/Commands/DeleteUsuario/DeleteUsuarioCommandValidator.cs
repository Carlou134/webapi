using FluentValidation;
using Microsoft.EntityFrameworkCore;
using webapi.Context;

namespace webapi.Features.Usuarios.Commands.DeleteUsuario
{
    public class DeleteUsuarioCommandValidator : AbstractValidator<DeleteUsuarioCommand>
    {
        public DeleteUsuarioCommandValidator(UsersContext context)
        {
            RuleFor(u => u.Id).NotEmpty().WithMessage("El identificador es requerido");
        }
    }
}
