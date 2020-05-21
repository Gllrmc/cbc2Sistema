using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Operaciones
{
    public class AsientoMap : IEntityTypeConfiguration<Asiento>
    {
        public void Configure(EntityTypeBuilder<Asiento> builder)
        {
            builder.ToTable("asientos")
                .HasKey(u => u.Id);
            builder.HasOne(a => a.empresa)
                .WithMany(d => d.asientos)
                .HasForeignKey(a => a.empresaId);
        }
    }
}
