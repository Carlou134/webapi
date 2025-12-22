using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoEF.Models;


namespace webapi.Context.Configurations
{
    public class TareaConfiguration : IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> builder)
        {
            builder.ToTable("Tarea");
            builder.HasKey(p => p.TareaId);
            builder.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);
            builder.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Descripcion);
            builder.Property(p => p.Prioridad).HasConversion<int>();
            builder.Property(p => p.FechaCreacion);
            builder.Property(p => p.Puntos);
            builder.Ignore(p => p.Resumen);
        }
    }
}
