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
      public   DbSet<SolicitudSubsidio> SolicitudSubsidios => Set<SolicitudSubsidio>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicamentos>()
                .Property(m => m.Precio)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder); 
        }

    }
}
