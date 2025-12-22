using MediatR;

namespace webapi.Notifications.Usuario.UsuarioCreado
{
    public record UsuarioCreadoNotification(Guid usuarioId) : INotification;
}
