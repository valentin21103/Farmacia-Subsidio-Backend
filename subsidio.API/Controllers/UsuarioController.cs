using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using subsidio.Business.services;
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
    // 1. BUSCAR: Traemos la entidad completa (la "sucia" con password)
    var usuarioEntidad = await _usuarioSevice.ObtenerPorId(id);

    // 2. VALIDAR
    if (usuarioEntidad == null) 
    { 
        return NotFound("No se encontro el usuario"); 
    }

    // 3. CONVERTIR (El momento mágico ✨)
    // Aquí creas la caja limpia y le pasas los datos de la entidad
    var usuarioLimpio = new UsuarioResponseDTO
    {
        Nombre = usuarioEntidad.Nombre, // "Lo que saqué de la DB" va a "La caja limpia"
        Apellido = usuarioEntidad.Apellido,
        CorreoElectronico = usuarioEntidad.CorreoElectronico
        // ¡Y NO pongas password!
    };

    // 4. DEVOLVER LA CAJA LIMPIA
    return Ok(usuarioLimpio);
}




        [HttpGet]
        public async Task<ActionResult<List<UsuarioResponseDTO>>> ObtenerTodos()
        {
            // PASO 1: Traemos el camión con toda la carga sucia (Entidades con password)
            // 'usuariosEntidad' es una LISTA (List<Usuario>)
            var usuariosEntidad = await _usuarioSevice.ObtenerTodos();

            // PASO 2: Preparamos una caja nueva y vacía para poner lo limpio
            // Al principio, esta lista tiene 0 elementos.
            var listaLimpia = new List<UsuarioResponseDTO>();

            // PASO 3: La Cinta Transportadora (El Bucle)
            // Por cada 'u' (un usuario individual) que haya en la lista 'usuariosEntidad'...
            foreach (var u in usuariosEntidad)
            {
                // ... creamos un DTO limpio solo para ÉL ...
                var dto = new UsuarioResponseDTO
                {
                    Nombre = u.Nombre,       // Aquí SI funciona, porque 'u' es UNO solo
                    Apellido = u.Apellido,
                    CorreoElectronico = u.CorreoElectronico
                    // La password la ignoramos
                };

                // ... y lo guardamos en la caja nueva.
                listaLimpia.Add(dto);
            }

            // PASO 4: Entregamos la caja que llenamos
            return Ok(listaLimpia);
        }


        [HttpPost]
        // CAMBIO 1: Prometemos devolver un DTO, NO una entidad
        public async Task<ActionResult<UsuarioResponseDTO>> Crear(CrearUsuarioDTO usuarioDto)
        {
            // 1. El servicio hace el trabajo sucio (crear y guardar con password)
            var nuevoUsuario = await _usuarioSevice.Crear(usuarioDto);

            // 2. LIMPIEZA: Convertimos la entidad creada a un DTO limpio
            // (Para no devolver la contraseña en el JSON de respuesta)
            var usuarioLimpio = new UsuarioResponseDTO
            {
                Nombre = nuevoUsuario.Nombre,
                Apellido = nuevoUsuario.Apellido,
                CorreoElectronico = nuevoUsuario.CorreoElectronico
            };

            // 3. RETORNO SEGURO
            // Param 1: A qué método llamar para verlo después (ObtenerPorId)
            // Param 2: El ID necesario para ese método (nuevoUsuario.Id)
            // Param 3: EL OBJETO LIMPIO que verá el usuario en su pantalla
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

    }
}
