using subsidio.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace subsidio.Business.services
{
    public interface IMedicamentoService
    {
        // 1. Dame un ticket para una lista de todos los medicamentos
        Task<List<Medicamentos>> ObtenerTodos();

        // 2. Dame un ticket para UN medicamento (si es que existe el ID)
        Task<Medicamentos?> ObtenerPorId(int id);

        // 3. Recibe un objeto, lo guarda y me devuelve el ticket con el objeto final
        Task<Medicamentos> Crear(Medicamentos medicamento);

        // 4. Recibe un ID y me devuelve un ticket que dirá "Éxito" o "Fallo"
        Task<bool> Eliminar(int id);
    }
}
