using Microsoft.EntityFrameworkCore;
using ProyectoEF.Models;

namespace ProyectoEF.Context
{
    public class TareasContext : DbContext
    {
        public TareasContext(DbContextOptions options) : base(options) { }

        public DbSet<Tarea> Tarea { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Categoria> categoriasInit = new List<Categoria>();
            
            categoriasInit.Add(new Categoria() { 
                CategoriaId = Guid.Parse("240acf13-8d8c-433d-90e0-f9c8b1dea04b"), 
                Nombre = "Actividades pendientes", 
                Peso = 20 
            });
            categoriasInit.Add(new Categoria() { 
                CategoriaId = Guid.Parse("240acf13-8d8c-433d-90e0-f9c8b1dea05b"), 
                Nombre = "Actividades personales", 
                Peso = 50 
            });

            modelBuilder.Entity<Categoria>(categoria =>
            {
                categoria.ToTable("Categoria");
                categoria.HasKey(p => p.CategoriaId);
                categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
                categoria.Property(p => p.Descripcion);
                categoria.Property(p => p.Peso);
                categoria.HasData(categoriasInit);
            });

            List<Tarea> tareasInit = new List<Tarea>();

            tareasInit.Add(new Models.Tarea() { 
                TareaId = Guid.Parse("240acf13-8d8c-433d-90e0-f9c8b1dea04c"), 
                CategoriaId = Guid.Parse("240acf13-8d8c-433d-90e0-f9c8b1dea04b"), 
                Prioridad = Prioridad.Medio, 
                Titulo = "Pago de servicios públicos", 
                FechaCreacion = new DateTime(2025, 12, 9)
            });
            tareasInit.Add(new Models.Tarea() { 
                TareaId = Guid.Parse("240acf13-8d8c-433d-90e0-f9c8b1dea05c"), 
                CategoriaId = Guid.Parse("240acf13-8d8c-433d-90e0-f9c8b1dea05b"), 
                Prioridad = Prioridad.Bajo, 
                Titulo = "Terminar de ver pelicula en Netflix", 
                FechaCreacion = new DateTime(2025, 12, 9)
            });

            modelBuilder.Entity<Tarea>(tarea =>
            {
                tarea.ToTable("Tarea");
                tarea.HasKey(p => p.TareaId);
                tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
                tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
                tarea.Property(p => p.Descripcion);
                tarea.Property(p => p.Prioridad).HasConversion<int>();
                tarea.Property(p => p.FechaCreacion);
                tarea.Property(p => p.Puntos);
                tarea.Ignore(p => p.Resumen);
                tarea.HasData(tareasInit);
            });
        }
    }
}
