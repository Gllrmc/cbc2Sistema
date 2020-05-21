using Microsoft.EntityFrameworkCore;
using Sistema.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Maestros
{
    class AsocuentaMap : IEntityTypeConfiguration<Asocuenta>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Asocuenta> builder)
        {
            builder.ToTable("asocuentas")
                .HasKey(u => u.Id);
            builder.HasOne(a => a.bancuenta)
                .WithMany(p => p.asocuentas)
                .HasForeignKey(a => a.bancuentaId);
            builder.HasOne(a => a.concuenta)
                .WithMany(p => p.asocuentas)
                .HasForeignKey(a => a.concuentaId);
            builder.HasIndex(a => new { a.empresaId, a.orden })
                .IsUnique(true);
            builder.HasIndex(a => new { a.empresaId, a.bancuentaId })
                .IsUnique(true);
            builder.HasIndex(a => new { a.empresaId, a.concuentaId })
                .IsUnique(true);
        }
    }
}
