using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Maestros
{
    public class BancuentaMap : IEntityTypeConfiguration<Bancuenta>
    {
        public void Configure(EntityTypeBuilder<Bancuenta> builder)
        {
            builder.ToTable("bancuentas")
            .HasKey(u => u.Id);
            builder.HasOne(a => a.banco)
                .WithMany(d => d.bancuentas)
                .HasForeignKey(a => a.bancoId);
            builder.HasOne(a => a.empresa)
                .WithMany(d => d.bancuentas)
                .HasForeignKey(a => a.empresaId);
            builder.HasIndex(p => new { p.empresaId, p.bancoId, p.tipo, p.moneda, p.numcuenta })
                .IsUnique(true);
        }
    }
}
