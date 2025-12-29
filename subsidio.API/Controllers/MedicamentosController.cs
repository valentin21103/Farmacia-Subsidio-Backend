using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using subsidio.Business.services;
using subsidio.Dominio.DTOs;
using subsidio.Dominio.Entities;

namespace subsidio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentosController : ControllerBase
    {
        private readonly IMedicamentoService _medicamentoService;

        public MedicamentosController(IMedicamentoService medicamentoService    )
        {
            _medicamentoService = medicamentoService;
        }


        [HttpGet]

        public async Task<ActionResult<List<Medicamentos>>> ObtenerTodos()
        {
             var medicamentos = await _medicamentoService.ObtenerTodos();
            return Ok(medicamentos);

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Medicamentos>> ObtenerPorId(int id)
        {
            var medicamento = await _medicamentoService.ObtenerPorId(id);
            if (medicamento == null)
            {
                return NotFound("No existe el medicamento");
            }
            return Ok(medicamento);
        }


        // 3. POST: Crear nuevo (AQUÍ USAMOS TU DTO)
        [HttpPost]
        public async Task<ActionResult<Medicamentos>> Crear(CrearMedicamentoDTO medicamentoDto)
        {
            // Le pasamos la caja limpia (DTO) al servicio
            var nuevoMedicamento = await _medicamentoService.Crear(medicamentoDto);

            // Devolvemos código 201 (Created)
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoMedicamento.Id }, nuevoMedicamento);
        }

        // 4. DELETE: Borrar
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _medicamentoService.Eliminar(id);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
