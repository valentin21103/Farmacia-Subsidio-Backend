using Microsoft.EntityFrameworkCore;
using subsidio.Dominio.Entities;
using subsidio.Infraestructura.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace subsidio.Business.services
{
    public class MedicamentoService : IMedicamentoService
    {
        private readonly AppDbContext _context;

        /*
         private: Solo la clase MedicamentoService puede tocar esta variable. Nadie de afuera puede verla.

         readonly (Solo lectura): Significa que una vez que le das un valor (en el constructor), ya no se puede cambiar nunca más.
         */

        public MedicamentoService(AppDbContext context)
        {
           
            _context = context;

        }



        public async  Task<List<Medicamentos>> ObtenerTodos()
        {
            return await _context.Medicamentos.ToListAsync();
        }

        public async Task<Medicamentos> Crear(Medicamentos medicamento)
        {
            _context.Medicamentos.Add(medicamento);

            await _context.SaveChangesAsync();
            return medicamento;
        }

        public async Task<bool> Eliminar(int id)
        {
            // Primero buscamos si existe
            var medicamento = await _context.Medicamentos.FindAsync(id);

            // Si es null, no hay nada que borrar
            if (medicamento == null) return false;

            // Si existe, lo marcamos para borrar
            _context.Medicamentos.Remove(medicamento);

            // Confirmamos la eliminación en SQL
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Medicamentos?> ObtenerPorId(int id)
        {
            return await _context.Medicamentos.FindAsync(id);
        }

    }
}
