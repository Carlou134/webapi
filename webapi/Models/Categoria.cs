using System.Text.Json.Serialization;

namespace ProyectoEF.Models
{
    public class Categoria
    {
        public Guid CategoriaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Peso { get; set; }

        [JsonIgnore]
        public virtual ICollection<Tarea>? Tareas { get; set; } 
    }
}
