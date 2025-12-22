using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoEF.Models;

namespace webapi.Context.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");
            builder.HasKey(p => p.CategoriaId);
            builder.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Descripcion);
            builder.Property(p => p.Peso);
        }
    }
}
