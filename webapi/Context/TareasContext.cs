using Microsoft.EntityFrameworkCore;
using ProyectoEF.Models;
using System.Reflection;

namespace ProyectoEF.Context
{
    public class TareasContext : DbContext
    {
        public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Tarea> Tarea { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
