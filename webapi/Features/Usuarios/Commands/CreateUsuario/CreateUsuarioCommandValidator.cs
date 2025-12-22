using FluentValidation;
using Microsoft.EntityFrameworkCore;
using webapi.Context;

namespace webapi.Features.Usuarios.Commands.CreateUsuario
{
    public class CreateUsuarioCommandValidator : AbstractValidator<CreateUsuarioCommand>
    {
        private readonly UsersContext _context;

        public CreateUsuarioCommandValidator(UsersContext context)
        {
            _context = context!;

            RuleFor(x => x.Nombre).
                NotEmpty().WithMessage("El nombre de usuario es requerido")
                .MustAsync(async (nombre, cancellationToken) => 
                !await _context.Usuario.AnyAsync(u => u.NombreUsuario == nombre, cancellationToken).ConfigureAwait(false))
                .WithMessage("El nombre de usuario ya está registrado");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo es requerido")
                .EmailAddress().WithMessage("El formato del email no es válido")
                .MustAsync(async (email, cancellationToken) => 
                !await _context.Usuario.AnyAsync(u => u.Email == email, cancellationToken).ConfigureAwait(false))
                .WithMessage("El correo ya está registrado");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida")
                .MinimumLength(8).WithMessage("La contraseña debe de tener como mínimo 6 caracteres");
        }
    }
}
