using subsidio.Dominio.DTOs;
using subsidio.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace subsidio.Business.services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> ObtenerTodos();

        Task<Usuario?> ObtenerPorId(int id);

        Task<Usuario> Crear(CrearUsuarioDTO crearUsuario);

        Task<bool> Eliminar(int id);

    }
}
