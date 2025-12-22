using System.Text.Json.Serialization;

namespace webapi.Models
{
    public class Rol
    {
        public Guid IdRol { get; set; }
        public string NombreRol { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Usuario>? Users { get; set; }
    }
}
