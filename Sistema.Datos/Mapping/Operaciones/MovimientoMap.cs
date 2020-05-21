using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Operaciones
{
    public class MovimientoMap : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> builder)
        {
            builder.ToTable("movimientos")
                .HasKey(u => u.Id);
            builder.HasOne(a => a.empresa)
                .WithMany(d => d.movimientos)
                .HasForeignKey(a => a.empresaId);
            builder.HasOne(a => a.asiento)
                .WithMany(d => d.movimientos)
                .HasForeignKey(a => a.asientoId);
            builder.HasOne(a => a.lote)
                .WithMany(d => d.movimientos)
                .HasForeignKey(a => a.loteId);
            builder.HasOne(a => a.grpconcepto)
                .WithMany(b => b.movimientos)
                .HasForeignKey(a => a.grpconceptoId);
        }
    }
}
