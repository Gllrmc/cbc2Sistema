using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Maestros;

namespace Sistema.Datos.Mapping.Maestros
{
    public class PaisMap : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("paises")
            .HasKey(u => u.Id);
            builder.Property(u => u.nombre)
            .HasMaxLength(50);
        }
    }
}
