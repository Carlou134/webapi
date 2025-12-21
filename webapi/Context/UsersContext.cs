using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Context
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> option) : base(option) { }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var Roles = new List<Rol>()
            {
                new Rol
                {
                    IdRol = Guid.Parse("240acf13-8d8c-433d-90e0-f9c8b1dea10c"),
                    NombreRol = "ADMIN"
                },
                new Rol
                {
                    IdRol = Guid.Parse("240acf13-8d8c-433d-90e0-f9c8b1dea11c"),
                    NombreRol = "USER"
                }
            };

            modelBuilder.Entity<Rol>(rol =>
            {
                rol.ToTable("Rol", "dbo");
                rol.HasKey(p => p.IdRol);
                rol.HasData(Roles);
            });


            var Usuarios = new List<Usuario>() {
                new Usuario
                {
                    UsuarioId = Guid.Parse("240acf13-8d8c-433d-90e0-f9c8b1dea20c"),
                    RolId = Guid.Parse("240acf13-8d8c-433d-90e0-f9c8b1dea10c"),
                    NombreUsuario = "Administrador",
                    Email = "Prueba@Gmail.com",
                    Password = "$2a$12$lBS.LD7m9wNn8eAtt9LZl.y5Fe/GqTqWrwSEnhiBNhr.DTBGqpt2C"
                }
            };

            modelBuilder.Entity<Usuario>(usuario =>
            {
                usuario.ToTable("Usuario", "dbo");
                usuario.HasKey(p => p.UsuarioId);
                usuario.Property(p => p.NombreUsuario).IsRequired().HasMaxLength(200);
                usuario.Property(p => p.Email).IsRequired().HasMaxLength(300);
                usuario.Property(p => p.Password).IsRequired().HasMaxLength(200);
                usuario.HasOne(p => p.Rol).WithMany(p => p.Users).HasForeignKey(p => p.RolId);
                usuario.HasData(Usuarios);
            });
        }

        internal async Task FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
