using System;
using System.Collections.Generic;
using System.Text;

namespace subsidio.Dominio.Entities
{
    public  class Usuario
    {

        public int Id { get; set; }

        public required string Nombre { get; set; }

        public required string Apellido { get; set; }

        public  required string CorreoElectronico { get; set; }

        public required string Contrasena { get; set; }

        public int Edad { get; set; }

        public Enums.TipoDeGenero Genero { get; set; }

        public Enums.Nivel Roll { get; set; }
    }
}
