using ProyectoEF.Context;
using ProyectoEF.Models;

namespace webapi.Services
{
    public class TareaService : ITareaService
    {
        private readonly TareasContext context;

        public TareaService(TareasContext dbContext)
        {
            context = dbContext;
        }

        public IEnumerable<Tarea> Get()
        {
            return context.Tarea;
        }

        public async Task Save(Tarea tarea)
        {
            await context.AddAsync(tarea);
            await context.SaveChangesAsync();
        }

        public async Task Update(Guid id, Tarea tarea)
        {
            var tareaActual = context.Tarea.Find(id);

            if (tareaActual != null)
            {
                tareaActual.CategoriaId = tarea.CategoriaId;
                tareaActual.Titulo = tarea.Titulo;
                tareaActual.Descripcion = tarea.Descripcion;
                tareaActual.Prioridad = tarea.Prioridad;
                tareaActual.Puntos = tarea.Puntos;

                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid id)
        {
            var tareaActual = context.Tarea.Find(id);

            if (tareaActual != null)
            {
                context.Remove(tareaActual);
                await context.SaveChangesAsync();
            }
        }

    }

    public interface ITareaService
    {
        IEnumerable<Tarea> Get();
        Task Save(Tarea tarea);
        Task Update(Guid id, Tarea tarea);
        Task Delete(Guid id);
    }
}
