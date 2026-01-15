using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using subsidio.Business.services;
using subsidio.Dominio.DTOs;
using subsidio.Dominio.Entities;
using static subsidio.Dominio.Enums;
using System.Linq; // <--- IMPORTANTE PARA QUE FUNCIONE EL .Select

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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<List<SolicitudResponseDTO>>> ObtenerSolicitudesDelUsuario(int usuarioId)
        {
            // 1. Traemos la data cruda (que YA incluye el medicamento gracias a tu servicio)
            var solicitudes = await _solicitud.ObtenerPorUsuario(usuarioId);

            if (solicitudes == null || !solicitudes.Any())
            {
                return Ok(new List<SolicitudResponseDTO>());
            }

            var respuesta = solicitudes.Select(s => new SolicitudResponseDTO
            {
                SolicitudId = s.Id,
                UsuarioId = s.SolicitanteId, 

                MedicamentoNombre = s.MedicamentoSolicitado != null ? s.MedicamentoSolicitado.Nombre : "Desconocido",
                MedicamentoPrecio = s.MedicamentoSolicitado != null ? s.MedicamentoSolicitado.Precio : 0,

                Estado = s.Estado.ToString(),
                FechaSolicitud = s.FechaSolicitud
            }).ToList();

            return Ok(respuesta);
        }

        [HttpPut("{id}/estado")]
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