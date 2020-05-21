using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Maestros;

namespace Sistema.Datos.Mapping.Maestros
{
    public class EmpresaMap : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("empresas")
                .HasKey(u => u.Id);
            builder.Property(u => u.nombre)
                .HasMaxLength(50);
            builder.Property(u => u.cuit)
                .HasMaxLength(11);
            builder.Property(u => u.direccion)
                .HasMaxLength(70);
            builder.Property(u => u.localidad)
                .HasMaxLength(50);
            builder.Property(u => u.cpostal)
                .HasMaxLength(8);
            builder.Property(u => u.telefono)
                .HasMaxLength(50);
            builder.Property(u => u.email)
                .HasMaxLength(50);
            builder.Property(u => u.webpage)
                .HasMaxLength(50);
        }
    }
}

