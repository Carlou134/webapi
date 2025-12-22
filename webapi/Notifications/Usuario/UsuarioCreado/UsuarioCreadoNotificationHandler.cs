using MediatR;

namespace webapi.Notifications.Usuario.UsuarioCreado
{
    public class UsuarioCreadoNotificationHandler : INotificationHandler<UsuarioCreadoNotification>
    {
        public Task Handle(
            UsuarioCreadoNotification notification,
            CancellationToken cancellationToken
            )
        {
            Console.WriteLine($"[Event] Usuario creado: {notification.usuarioId}");
            return Task.CompletedTask;
        }
    }
}
