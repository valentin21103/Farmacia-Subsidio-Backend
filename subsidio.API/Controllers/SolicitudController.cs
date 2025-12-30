using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using subsidio.Business.services;
using subsidio.Dominio.DTOs;
using subsidio.Dominio.Entities;
using static subsidio.Dominio.Enums;

namespace subsidio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly SolicitudService _solicitud;

        public SolicitudController(SolicitudService solicitu)
        {
            _solicitud = solicitu;
        }

        [HttpPost]

        public async Task<ActionResult> Crear([FromBody] SolicitudDTO DTO)
        {
            try
            {
                var nuevaSolicitud = await _solicitud.CrearSolicitud(DTO);

                return Ok(nuevaSolicitud);

            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("usuario/{usuarioId}")]

        public async Task<ActionResult> ObtenerSolicitudesDelUsuario(int usuarioId)
        {
            var solicitudes = await _solicitud.ObtenerPorUsuario(usuarioId);

            if (solicitudes == null || !solicitudes.Any())
            {
                // Opcional: devolver Ok con lista vacía o NotFound
                return Ok(new List<SolicitudSubsidio>());
            }

            return Ok(solicitudes);
        }

        [HttpPut("{id}/estado")] // PUT: api/Solicitud/1/estado
        public async Task<IActionResult> ActualizarEstado(int id, [FromBody] EstadoSolicitud nuevoEstado)
        {
            try
            {
                await _solicitud.CambiarEstado(id, nuevoEstado);
                return Ok($"Estado actualizado a: {nuevoEstado}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
