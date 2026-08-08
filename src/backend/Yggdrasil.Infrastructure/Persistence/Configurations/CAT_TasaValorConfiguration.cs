using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class CAT_TasaValorConfiguration : IEntityTypeConfiguration<CAT_TasaValor>
{
    public void Configure(EntityTypeBuilder<CAT_TasaValor> builder)
    {
        // Nombre de la tabla
        builder.ToTable("CAT_TasaValor");

        // Llave primaria
        builder.HasKey(t => t.Id);

        // Configurar propiedades
        builder.Property(t => t.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .UseIdentityColumn(); // Auto-incrementable

        builder.Property(t => t.TasaId)
            .HasColumnName("TasaId")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(t => t.ValorTasa)
            .HasColumnName("ValorTasa")
            .HasColumnType("decimal(18,6)") // Alta precisión para tasas de interés
            .IsRequired();

        builder.Property(t => t.Fecha)
            .HasColumnName("Fecha")
            .HasColumnType("datetime")
            .IsRequired(false); // Nullable

        builder.Property(t => t.FechaRegistro)
            .HasColumnName("FechaRegistro")
            .HasColumnType("datetime")
            .IsRequired()
            .HasDefaultValueSql("GETDATE()"); // Valor por defecto fecha actual

        // Relación con CAT_Tasa
        builder.HasOne(t => t.CAT_Tasa)
            .WithMany(t=>t.CAT_TasaValor)
            .HasForeignKey(t => t.TasaId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        // Índices
        builder.HasIndex(t => t.TasaId)
            .HasDatabaseName("IX_CAT_TasaValor_TasaId");

        builder.HasIndex(t => t.Fecha)
            .HasDatabaseName("IX_CAT_TasaValor_Fecha");

        // Índice compuesto para búsquedas comunes (tasa + fecha)
        builder.HasIndex(t => new { t.TasaId, t.Fecha })
            .HasDatabaseName("IX_CAT_TasaValor_TasaId_Fecha");

        // Índice único para evitar valores duplicados de tasa en la misma fecha
        builder.HasIndex(t => new { t.TasaId, t.Fecha })
            .IsUnique()
            .HasDatabaseName("UK_CAT_TasaValor_TasaId_Fecha");

    }
}