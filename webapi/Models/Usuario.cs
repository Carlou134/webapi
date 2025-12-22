using System.Text.Json.Serialization;

namespace webapi.Models
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; }
        public Guid RolId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        [JsonIgnore]
        public Rol? Rol { get; set; }
    }
}
