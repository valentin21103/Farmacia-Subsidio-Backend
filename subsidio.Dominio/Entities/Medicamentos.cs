using System;
using System.Collections.Generic;
using System.Text;

namespace subsidio.Dominio.Entities
{
    public class Medicamentos
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int CantidadEnStock { get; set; }

        public override string ToString()
        {
            return $"Medicamento: {Nombre}, Descripción: {Descripcion}, Precio: {Precio:C}, Cantidad en Stock: {CantidadEnStock}";
        }

    }
}
