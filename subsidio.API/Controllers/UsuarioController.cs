using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using subsidio.Business.services;
using subsidio.Dominio;
using subsidio.Dominio.DTOs;
using subsidio.Dominio.Entities;

namespace subsidio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioSevice;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioSevice = usuarioService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponseDTO>> ObtenerPorId(int id)
        {
            // 1. BUSCAR
            var usuarioEntidad = await _usuarioSevice.ObtenerPorId(id);

            // 2. VALIDAR
            if (usuarioEntidad == null)
            {
                return NotFound("No se encontro el usuario");
            }

            // 3. CONVERTIR (Con ID incluido)
            var usuarioLimpio = new UsuarioResponseDTO
            {
                Id = usuarioEntidad.Id, // <--- ¡IMPORTANTE!
                Nombre = usuarioEntidad.Nombre,
                Apellido = usuarioEntidad.Apellido,
                CorreoElectronico = usuarioEntidad.CorreoElectronico,
                Roll = usuarioEntidad.Roll.ToString(),
                Genero = usuarioEntidad.Genero.ToString()
            };

            // 4. DEVOLVER
            return Ok(usuarioLimpio);
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioResponseDTO>>> ObtenerTodos()
        {
            var usuariosEntidad = await _usuarioSevice.ObtenerTodos();
            var listaLimpia = new List<UsuarioResponseDTO>();

            foreach (var u in usuariosEntidad)
            {
                var dto = new UsuarioResponseDTO
                {
                    Id = u.Id, // <--- ¡IMPORTANTE!
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    CorreoElectronico = u.CorreoElectronico,
                    Roll = u.Roll.ToString(),
                    Genero = u.Genero.ToString()
                };
                listaLimpia.Add(dto);
            }

            return Ok(listaLimpia);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioResponseDTO>> Crear(CrearUsuarioDTO usuarioDto)
        {
            var nuevoUsuario = await _usuarioSevice.Crear(usuarioDto);

            var usuarioLimpio = new UsuarioResponseDTO
            {
                Id = nuevoUsuario.Id, // <--- ¡IMPORTANTE!
                Nombre = nuevoUsuario.Nombre,
                Apellido = nuevoUsuario.Apellido,
                CorreoElectronico = nuevoUsuario.CorreoElectronico,
                Roll = nuevoUsuario.Roll.ToString(),
                Genero = nuevoUsuario.Genero.ToString()
            };

            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoUsuario.Id }, usuarioLimpio);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var UsuarioExiste = await _usuarioSevice.Eliminar(id);

            if (!UsuarioExiste)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioResponseDTO>> Login(LoginDTO loginDto)
        {
            var usuarioEntidad = await _usuarioSevice.Login(loginDto);

            // CASO 1: ERROR
            if (usuarioEntidad == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            // CASO 2: ÉXITO (Aquí es donde fallaba antes por falta de ID)
            var usuarioLimpio = new UsuarioResponseDTO
            {
                Id = usuarioEntidad.Id, // <--- ¡CRUCIAL PARA EL LOGIN! 🔑
                Nombre = usuarioEntidad.Nombre,
                Apellido = usuarioEntidad.Apellido,
                CorreoElectronico = usuarioEntidad.CorreoElectronico,
                Roll = usuarioEntidad.Roll.ToString(),
                Genero = usuarioEntidad.Genero.ToString()
            };

            return Ok(usuarioLimpio);
        }
    }
}
