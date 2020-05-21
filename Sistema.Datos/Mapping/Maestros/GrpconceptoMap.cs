using Microsoft.EntityFrameworkCore;
using Sistema.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Maestros
{
    class GrpconceptoMap : IEntityTypeConfiguration<Grpconcepto>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Grpconcepto> builder)
        {
            builder.ToTable("grpconceptos")
                .HasKey(u => u.Id);
            builder.Property(u => u.nombre)
                .HasMaxLength(50);
            builder.HasIndex(a => new { a.empresaId, a.orden })
                .IsUnique(true);
            builder.HasIndex(a => new { a.empresaId, a.nombre })
                .IsUnique(true);
        }
    }
}
