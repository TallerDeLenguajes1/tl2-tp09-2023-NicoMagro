using Microsoft.AspNetCore.Mvc;
using TP9.Repositorios;
using TP9.Clases;

namespace TP9.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableroController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly ITableroRepository repository;

        public TableroController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
            repository = new TableroRepository();
        }

        [HttpPost]
        [Route("CreateTablero")]
        public ActionResult<Tablero> Create(Tablero tablero)
        {
            repository.Create(tablero);
            return Ok($"Tablero {tablero.Nombre} Creado");
        }

        [HttpGet]
        public ActionResult<List<Tablero>> GetAll()
        {
            List<Tablero> tableros = repository.GetAll();
            if (tableros == null)
            {
                return BadRequest("No se encontraron tableros");
            }
            else
            {
                return Ok(tableros);
            }
        }
    }
}