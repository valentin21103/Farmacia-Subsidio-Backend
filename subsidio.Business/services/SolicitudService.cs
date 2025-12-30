using Microsoft.EntityFrameworkCore;
using subsidio.Dominio.DTOs;
using subsidio.Dominio.Entities;
using subsidio.Infraestructura.Data;
using System;
using System.Collections.Generic;
using System.Text;
using static subsidio.Dominio.Enums;

namespace subsidio.Business.services
{
    public class SolicitudService
    {
        private readonly AppDbContext _context;

        public SolicitudService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<SolicitudSubsidio> CrearSolicitud(SolicitudDTO CrearSolicitud)
        {
            // validar usuario

            var usuario = await _context.Usuarios.FindAsync(CrearSolicitud.UsuarioId);
            if (usuario == null) throw new Exception("Usuario no encontrado");

            var medicamento = await _context.Medicamentos.FindAsync(CrearSolicitud.MedicamentoId);
            if (medicamento == null) throw new Exception("Medicamento no encontrado");

            var NuevaSolicitud = new SolicitudSubsidio
            {
                SolicitanteId = CrearSolicitud.UsuarioId,
                Solicitante = usuario,
                MedicamentoSolicitadoId = CrearSolicitud.MedicamentoId,
                MedicamentoSolicitado = medicamento,
                FechaSolicitud = DateTime.UtcNow,
                Estado = EstadoSolicitud.Pendiente

            };

            _context.SolicitudSubsidios.Add(NuevaSolicitud);
            await _context.SaveChangesAsync();
            return (NuevaSolicitud);
        }

        public async Task<List<SolicitudSubsidio>> ObtenerPorUsuario(int usuarioId)
        {
            return await _context.SolicitudSubsidios
               .Where(s => s.SolicitanteId == usuarioId) // Filtra solo las de este usuario
               .Include(s => s.MedicamentoSolicitado)    // <--- ¡CLAVE! Carga los datos del remedio (Nombre, Precio)
               .OrderByDescending(s => s.FechaSolicitud) // Las más nuevas primero
               .ToListAsync();
        }

        public async Task CambiarEstado(int solicitudId, EstadoSolicitud nuevoEstado)
        {
            var solicitud = await _context.SolicitudSubsidios.FindAsync(solicitudId);
            if (solicitud == null) throw new Exception("Solicitud no encontrada");

            solicitud.Estado = nuevoEstado; // Aquí ocurre la magia
            await _context.SaveChangesAsync();
        }
    }
}
