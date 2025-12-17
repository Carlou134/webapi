using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Context
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> option) : base(option) { }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(usuario =>
            {
                usuario.ToTable("Usuario", "dbo");
                usuario.HasKey(p => p.UsuarioId);
                usuario.Property(p => p.Email).IsRequired().HasMaxLength(300);
                usuario.Property(p => p.Password).IsRequired().HasMaxLength(100);
                usuario.HasOne(p => p.Rol).WithMany(p => p.Users).HasForeignKey(p => p.RolId);
            });

            modelBuilder.Entity<Rol>(rol =>
            {
                rol.ToTable("Rol", "dbo");
                rol.HasKey(p => p.IdRol);
            });
        }
    }
}
