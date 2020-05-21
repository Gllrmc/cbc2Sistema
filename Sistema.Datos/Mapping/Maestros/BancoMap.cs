using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Maestros
{
    public class BancoMap : IEntityTypeConfiguration<Banco>
    {
        public void Configure(EntityTypeBuilder<Banco> builder)
        {
            builder.ToTable("bancos")
                .HasKey(u => u.Id);
        }
    }
}
