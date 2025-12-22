using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using webapi.Models;

namespace webapi.Context.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario", "dbo");
            builder.HasKey(p => p.UsuarioId);
            builder.Property(p => p.NombreUsuario).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(300);
            builder.Property(p => p.Password).IsRequired().HasMaxLength(200);
            builder.HasOne(p => p.Rol).WithMany(p => p.Users).HasForeignKey(p => p.RolId);
        }
    }
}
