using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Maestros
{
    public class ConcuentaMap : IEntityTypeConfiguration<Concuenta>
    {
        public void Configure(EntityTypeBuilder<Concuenta> builder)
        {
            builder.ToTable("concuentas")
            .HasKey(u => u.Id);
            builder.HasOne(a => a.empresa)
                .WithMany(d => d.concuentas)
                .HasForeignKey(a => a.empresaId);
            builder.HasIndex(p => new { p.empresaId, p.apporigen, p.moneda, p.numcuenta })
                .IsUnique(true);
        }
    }
}
