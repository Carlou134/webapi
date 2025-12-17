using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoEF.Context;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        private readonly IHelloWorldService helloWorldService;
        private readonly TareasContext dbContext;

        public HelloWorldController(IHelloWorldService helloWorld, TareasContext db)
        {
            helloWorldService = helloWorld;
            dbContext = db;
        }

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            return Ok(helloWorldService.GetHelloWorld());
        }

        [HttpGet("createdb")]
        public IActionResult CreateDatabase()
        {
            dbContext.Database.EnsureCreated();
            return Ok();
        }
    }
}
