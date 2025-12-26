using System;
using System.Collections.Generic;
using System.Text;

namespace subsidio.Dominio.Entities
{
    public class SolicitudSubsidio
    {
        public int Id { get; set; }

        // --- PAREJA 1: El Usuario ---
        public int SolicitanteId { get; set; } // <--- EL DNI (Para la Base de Datos)
        public required Usuario Solicitante { get; set; } // <--- LA PERSONA (Para C#)

        // --- PAREJA 2: El Medicamento ---
        public int MedicamentoSolicitadoId { get; set; } // <--- EL CODIGO (Para la Base de Datos)
        public required Medicamentos MedicamentoSolicitado { get; set; } // <--- LA CAJA (Para C#)

        public DateTime FechaSolicitud { get; set; } = DateTime.UtcNow; // <--- Otro valor por defecto (pone la hora actual sola)
        public Enums.EstadoSolicitud Estado { get; set; } = Enums.EstadoSolicitud.Pendiente;    // al hacer esto dejamos un valor por defecto
    }
}
