using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;


public class CAT_CalendarioLaboralConfiguration : IEntityTypeConfiguration<CAT_CalendarioLaboral>
{
    public void Configure(EntityTypeBuilder<CAT_CalendarioLaboral> builder)
    {
        builder.ToTable("CAT_CalendarioLaboral");

        // Clave primaria
        builder.HasKey(x => x.Id);

        // Configuración de propiedades
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()  // Auto-incrementable
            .IsRequired();

        builder.Property(x => x.Fecha)
            .IsRequired()
            .HasColumnType("date");  // Solo fecha, sin hora

        builder.Property(x => x.EsHabil)
            .IsRequired();

        builder.Property(x => x.Descripcion)
            .HasMaxLength(500)  // Longitud máxima
            .IsRequired(false); // Permite nulos

        // Índice único para evitar fechas duplicadas 
        builder.HasIndex(x => x.Fecha)
            .IsUnique()
            .HasDatabaseName("IX_PSV_CalendarioLaboral_Fecha");

    }
}

