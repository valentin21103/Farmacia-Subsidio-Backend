using System;
using System.Collections.Generic;
using System.Text;

namespace subsidio.Dominio.DTOs
{
    public class SolicitudResponseDTO
    {
        public int SolicitudId { get; set; }
        public int UsuarioId { get; set; }

        public string? UsuarioNombre { get; set; }
        public required string MedicamentoNombre { get; set; }
        public decimal MedicamentoPrecio { get; set; }

        // 👇 FALTABA "required"
        public required string Estado { get; set; }
        public DateTime FechaSolicitud { get; set; }
    }
}
