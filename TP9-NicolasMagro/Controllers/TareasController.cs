using Microsoft.AspNetCore.Mvc;
using TP9.Clases;
using TP9.Repositorios;

namespace TP9.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly ITareaRepository repository;

        public TareasController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
            repository = new TareaRepository();
        }

        [HttpPost]
        [Route("CreateTarea")]
        public ActionResult<Tarea> Create(int idTablero, Tarea task)
        {
            repository.Create(idTablero, task);
            return Ok($"Tarea {task.Nombre} creada");
        }

        [HttpPut]
        [Route("ModificarTarea")]
        public ActionResult<Tarea> Update(int id, Tarea task)
        {
            repository.Update(id, task);
            return Ok($"Tarea {id} Actualizada");
        }

        [HttpPut]
        [Route("ModificarEstado")]
        public ActionResult<Tarea> UpdateEstado(int id, EstadoTarea estado)
        {
            Tarea task = repository.GetById(id);
            task.Estado = estado;
            repository.Update(id, task);
            return Ok($"Tarea {id} Actualizada");
        }

        [HttpDelete]
        [Route("EliminarTarea")]
        public IActionResult Remove(int id)
        {
            repository.Remove(id);
            return Ok($"Tarea {id} eliminada");
        }

        [HttpGet]
        [Route("CantidadTareasEnUnEstado")]
        public ActionResult<int> CantTareasEstado(EstadoTarea estado)
        {
            var listado = repository.GetAll().Where(p => p.Estado == estado);
            if (listado == null)
            {
                return BadRequest($"No se encontraron tareas con el estado {estado}");
            }
            else
            {
                return Ok(listado.Count());
            }
        }

        [HttpGet]
        [Route("ListarTareasUsuario{id}")]
        public ActionResult<List<Tarea>> GetByUsuario(int id)
        {
            var listado = repository.GetByUsuario(id);
            if (listado == null)
            {
                return BadRequest($"No se encontraron tareas asignadas al usuario {id}");
            }
            else
            {
                return Ok(listado);
            }
        }

        [HttpGet]
        [Route("ListarTareasTablero{id}")]
        public ActionResult<List<Tarea>> GetByTablero(int id)
        {
            var listado = repository.GetByTablero(id);
            if (listado == null)
            {
                return BadRequest($"No se encontraron tareas asignadas al usuario {id}");
            }
            else
            {
                return Ok(listado);
            }
        }
    }
}