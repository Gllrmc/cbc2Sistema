using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Maestros
{
    public class AppconfigMap : IEntityTypeConfiguration<Appconfig>
    {
        public void Configure(EntityTypeBuilder<Appconfig> builder)
        {
            builder.ToTable("appconfig")
            .HasKey(u => u.id);
        }
    }
}
