using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class CAT_TasaConfiguration : IEntityTypeConfiguration<CAT_Tasa>
{
    public void Configure(EntityTypeBuilder<CAT_Tasa> builder)
    {
        // Nombre de la tabla
        builder.ToTable("CAT_Tasa");

        // Llave primaria
        builder.HasKey(t => t.Id);

        // Configurar propiedades
        builder.Property(t => t.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .UseIdentityColumn(); // Auto-incrementable

        builder.Property(t => t.ValorTasa)
            .HasColumnName("ValorTasa")
            .HasColumnType("decimal(8, 4)") // Precisión 8, escala 4
            .IsRequired();

        builder.Property(t => t.NomTasa)
            .HasColumnName("NomTasa")
            .HasColumnType("nvarchar")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(t => t.EsVariable)
            .HasColumnName("EsVariable")
            .HasColumnType("bit")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(t => t.Activo)
            .HasColumnName("Activo")
            .HasColumnType("bit")
            .IsRequired()
            .HasDefaultValue(true);

        // Relación con CAT_TasaValor (uno a muchos)
        builder.HasMany(t => t.CAT_TasaValor)
            .WithOne(tv => tv.CAT_Tasa)
            .HasForeignKey(tv => tv.TasaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(t => t.NomTasa)
            .IsUnique()
            .HasDatabaseName("IX_CAT_Tasa_NomTasa_Unique");

        builder.HasIndex(t => t.Activo)
            .HasDatabaseName("IX_CAT_Tasa_Activo");

        builder.HasIndex(t => t.EsVariable)
            .HasDatabaseName("IX_CAT_Tasa_EsVariable");

        // Índice compuesto
        builder.HasIndex(t => new { t.Activo, t.EsVariable })
            .HasDatabaseName("IX_CAT_Tasa_Activo_EsVariable");

        
    }
}
