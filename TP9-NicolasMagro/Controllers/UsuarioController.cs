using Microsoft.AspNetCore.Mvc;
using TP9.Clases;
using TP9.Repositorios;

namespace TP9.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioRepository repository;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
            repository = new UsuarioRepository();
        }

        [HttpPost]
        [Route("CreateUser")]
        public ActionResult<Usuario> Create(Usuario user)
        {
            repository.Create(user);
            return Ok($"Usuario {user.Nombre} creado correctamente");
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<List<Usuario>> GetAll()
        {
            var usuarios = repository.GetAll();

            if (usuarios == null)
            {
                return BadRequest("No se obtuvo ningun resultado");
            }
            else
            {
                return Ok(usuarios);
            }
        }

        [HttpGet]
        [Route("GetById")]
        public ActionResult<Usuario> GetById(int id)
        {
            var usuario = repository.GetById(id);

            if (usuario == null)
            {
                return BadRequest("No se encontro ningun usuario con el id ingresado");
            }
            else
            {
                return Ok(usuario);
            }
        }

        [HttpPut]
        [Route("UpdateName")]
        public ActionResult<Usuario> UpdateName(int id, Usuario user)
        {
            repository.Update(id, user);
            return Ok("El usuario fue modificado");
        }
    }
}