using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Maestros;

namespace Sistema.Datos.Mapping.Maestros
{
    public class PersonaMap : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("personas")
                .HasKey(u => u.Id);
            builder.Property(u => u.nombre)
                .HasMaxLength(50);
            builder.Property(u => u.domicilio)
                .HasMaxLength(70);
            builder.Property(u => u.localidad)
                .HasMaxLength(50);
            builder.Property(u => u.cpostal)
                .HasMaxLength(8);
            builder.Property(u => u.emailpersonal)
                .HasMaxLength(50);
            builder.Property(u => u.telefonopersonal)
                .HasMaxLength(20);
            builder.Property(u => u.tipodocumento)
                .HasMaxLength(3);
            builder.Property(u => u.numdocumento)
                .HasMaxLength(20);
        }
    }
}

