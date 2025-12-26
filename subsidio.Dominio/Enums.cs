using System;
using System.Collections.Generic;
using System.Text;

namespace subsidio.Dominio
{
    public class Enums
    {
        public enum TipoDeGenero
        {
            Masculino,
            Femenino
        }

        public enum Nivel
        {
            Administrador,
            Usuario
        }

        public enum EstadoSolicitud
        {
            Pendiente = 0, // Cuando recién la pide
            Aprobado = 1,  // El admin dijo SI
            Rechazado = 2  // El admin dijo NO
        }
    }
}
