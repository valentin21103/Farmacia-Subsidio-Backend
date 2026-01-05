using Microsoft.EntityFrameworkCore;
using subsidio.Dominio.DTOs;
using subsidio.Dominio.Entities;
using subsidio.Infraestructura.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace subsidio.Business.services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Usuario?> ObtenerPorId(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<List<Usuario>> ObtenerTodos()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> Crear(CrearUsuarioDTO dto)
        {
            var NuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                CorreoElectronico = dto.CorreoElectronico,
                Contrasena = dto.Contrasena

            };

            _context.Usuarios.Add(NuevoUsuario);
            await _context.SaveChangesAsync();
            return NuevoUsuario;
        }

        public async Task<bool> Eliminar(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null) return false;
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> Login(LoginDTO loginDto)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.CorreoElectronico == loginDto.Email && u.Contrasena == loginDto.Password);
        }
    }
}
