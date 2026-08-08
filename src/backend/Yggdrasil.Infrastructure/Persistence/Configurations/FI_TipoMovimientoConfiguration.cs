using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_TipoMovimientoConfiguration : IEntityTypeConfiguration<FI_TipoMovimiento>
{
    public void Configure(EntityTypeBuilder<FI_TipoMovimiento> builder)
    {
        builder.ToTable("FI_TipoMovimiento");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Clave)
            .IsRequired()
            .HasMaxLength(6)
            .HasColumnType("varchar(6)");

        builder.Property(e => e.NomTipoMovimiento)
            .IsRequired()
            .HasMaxLength(60)
            .HasColumnType("varchar(60)");

        builder.Property(e => e.GeneraIvaCapital)
            .HasDefaultValue(false);

        builder.Property(e => e.GeneraIvaInteres)
            .HasDefaultValue(false);

        builder.Property(e => e.GeneraMora)
            .HasDefaultValue(false);

        builder.Property(e => e.EsCargoInicial)
            .HasDefaultValue(false);

        builder.Property(e => e.EsConceptoFinanciado)
            .HasDefaultValue(false);

        builder.Property(e => e.Activo)
            .HasDefaultValue(true);

        builder.HasIndex(e => e.Clave)
            .IsUnique()
            .HasDatabaseName("IX_CAT_TipoMovimiento_Clave");
    }
}
