using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoEF.Models;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService categoriaService;

        public CategoriaController(ICategoriaService service)
        {
            categoriaService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(categoriaService.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Categoria categoria)
        {
            await categoriaService.Save(categoria);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Categoria categoria)
        {
            await categoriaService.Update(id, categoria);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await categoriaService.Delete(id);
            return Ok();
        }
    }
}
