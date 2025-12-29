using Microsoft.EntityFrameworkCore;
using subsidio.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace subsidio.Infraestructura.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

      public  DbSet<Usuario> Usuarios => Set<Usuario>();
      public  DbSet<Medicamentos> Medicamentos => Set<Medicamentos>();

        /*
          public DbSet<Medicamentos>: Esto le dice a C#: "Oye, esto va a ser una Tabla (DbSet) llena de objetos del tipo Medicamentos".

          Medicamentos (El segundo nombre): ¡ESTA ES LA CLAVE! 🔑 Este es el Nombre de la Propiedad. Es el "apodo" con el que vas a llamar a la tabla desde el código.
         */
        public DbSet<SolicitudSubsidio> SolicitudSubsidios => Set<SolicitudSubsidio>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicamentos>()
                .Property(m => m.Precio)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder); 
        }

    }
}
