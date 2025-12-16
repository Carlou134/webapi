using Microsoft.EntityFrameworkCore;
using ProyectoEF.Context;
using ProyectoEF.Models;

namespace webapi.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly TareasContext context;

        public CategoriaService(TareasContext dbContext)
        {
            context = dbContext;
        }

        public IEnumerable<Categoria> Get()
        {
            return context.Categoria;
        }

        public async Task Save(Categoria categoria)
        {
            await context.AddAsync(categoria);
            await context.SaveChangesAsync();
        }

        public async Task Update(Guid id, Categoria categoria)
        {
            var categoriaActual = context.Categoria.Find(id);

            if(categoriaActual != null)
            {
                categoriaActual.Nombre = categoria.Nombre;
                categoriaActual.Descripcion = categoria.Descripcion;
                categoriaActual.Peso = categoria.Peso;

                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid id)
        {
            var categoriaActual = context.Categoria.Find(id);

            if (categoriaActual != null)
            {
                context.Remove(categoriaActual);
                await context.SaveChangesAsync();
            }
        }
    }

    public interface ICategoriaService
    {
        IEnumerable<Categoria> Get();
        Task Save(Categoria categoria);
        Task Update(Guid id, Categoria categoria);
        Task Delete(Guid id);
    }
}
