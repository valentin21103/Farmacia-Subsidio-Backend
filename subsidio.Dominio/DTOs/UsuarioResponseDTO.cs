using System;
using System.Collections.Generic;
using System.Text;

namespace subsidio.Dominio.DTOs
{
    public class UsuarioResponseDTO
    {

        public int Id { get; set; }
        public required string Nombre { get; set; }

        public required string Apellido { get; set; }

        public required string CorreoElectronico { get; set; }


        public required string Roll { get; set; }

        public required string Genero { get; set; }
    }
}
