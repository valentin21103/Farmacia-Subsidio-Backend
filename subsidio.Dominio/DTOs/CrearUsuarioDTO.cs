using System;
using System.Collections.Generic;
using System.Text;

namespace subsidio.Dominio.DTOs
{
    public class CrearUsuarioDTO
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string CorreoElectronico { get; set; }
        public required string Contrasena { get; set; } // Aquí SÍ pedimos el password

        public required int Edad { get; set; }

        public required string Genero { get; set; }

    }
}
