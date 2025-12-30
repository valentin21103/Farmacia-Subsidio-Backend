using System;
using System.Collections.Generic;
using System.Text;

namespace subsidio.Dominio.DTOs
{
    public class SolicitudDTO
    {
        public required int UsuarioId { get; set; }
        public required int MedicamentoId { get; set; }
    }
}
