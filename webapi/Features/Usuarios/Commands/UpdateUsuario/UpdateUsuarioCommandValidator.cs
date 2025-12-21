using FluentValidation;
using Microsoft.EntityFrameworkCore;
using webapi.Context;

namespace webapi.Features.Usuarios.Commands.UpdateUsuario
{
    public class UpdateUsuarioCommandValidator : AbstractValidator<UpdateUsuarioCommand>
    {
        private readonly UsersContext _context;

        public UpdateUsuarioCommandValidator(UsersContext context)
        {
            _context = context!;

            RuleFor(u => u.Id).NotEmpty().WithMessage("El identificador debe de ser obligatorio");

            RuleFor(x => x.Nombre).
                NotEmpty().WithMessage("El nombre de usuario es requerido")
                .MustAsync(NombreUnico)
                .WithMessage("El nombre de usuario ya está registrado");


            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo es requerido")
                .EmailAddress().WithMessage("El formato del email no es válido")
                .MustAsync(EmailUnico)
                .WithMessage("El correo ya está registrado");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida")
                .MinimumLength(8).WithMessage("La contraseña debe de tener como mínimo 6 caracteres");
        }

        private async Task<bool> NombreUnico(
            UpdateUsuarioCommand command,
            string nombre,
            CancellationToken cancellationToken)
        {
            return !await _context.Usuario
                .AnyAsync(u => u.NombreUsuario == command.Nombre 
                && u.UsuarioId != command.Id, cancellationToken)
                .ConfigureAwait(false);
        }

        private async Task<bool> EmailUnico(
            UpdateUsuarioCommand command,
            string nombre,
            CancellationToken cancellationToken)
        {
            return !await _context.Usuario
                .AnyAsync(u => u.Email == command.Email && u.UsuarioId != command.Id)
                .ConfigureAwait(false);
        }
    }
}
