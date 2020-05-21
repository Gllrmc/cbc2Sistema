using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Maestros;

namespace Sistema.Datos.Mapping.Maestros
{
    public class ProvinciaMap : IEntityTypeConfiguration<Provincia>
    {
        public void Configure(EntityTypeBuilder<Provincia> builder)
        {
            builder.ToTable("provincias")
               .HasKey(u => u.Id);
            builder.Property(u => u.nombre)
                .HasMaxLength(50);
            builder.HasIndex(a => new { a.paisId, a.nombre })
                .IsUnique(true);
        }
    }
}

