using System;
using System.Collections.Generic;
using System.Text;
using static subsidio.Dominio.Enums;

namespace subsidio.Dominio.DTOs
{
    public class SolicitudDTO
    {
        public required int UsuarioId { get; set; }

        // 👇 ESTA ES LA QUE TE FALTABA O ESTABA MAL ESCRITA
        public required int MedicamentoId { get; set; }

        // Opcional: Puede tener un valor por defecto
        public EstadoSolicitud Estado { get; set; } = EstadoSolicitud.Pendiente;

    }
}
