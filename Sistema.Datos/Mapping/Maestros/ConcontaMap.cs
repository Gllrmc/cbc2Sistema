using Microsoft.EntityFrameworkCore;
using Sistema.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Maestros
{
    public class ConcontaMap : IEntityTypeConfiguration<Conconta>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Conconta> builder)
        {
            builder.ToTable("concontas")
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
