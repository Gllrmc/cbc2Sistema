using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Operaciones
{
    public class LoteMap : IEntityTypeConfiguration<Lote>
    {
        public void Configure(EntityTypeBuilder<Lote> builder)
        {
            builder.ToTable("lotes")
                .HasKey(u => u.Id);
            builder.HasOne(a => a.empresa)
                .WithMany(d => d.lotes)
                .HasForeignKey(a => a.empresaId);
            builder.HasOne(a => a.asocuenta)
                .WithMany(d => d.lotes)
                .HasForeignKey(a => a.asocuentaId);
        }
    }
}
