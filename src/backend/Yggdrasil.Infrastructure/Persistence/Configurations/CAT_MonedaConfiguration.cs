using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class CAT_MonedaConfiguration : IEntityTypeConfiguration<CAT_Moneda>
{
    public void Configure(EntityTypeBuilder<CAT_Moneda> builder)
    {
        builder.ToTable("CAT_Monedas");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.NomMoneda)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(e => e.ClaveMoneda)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnType("varchar(10)");

        builder.Property(e => e.PorDefecto)
            .HasDefaultValue(false);

        builder.HasIndex(e => e.ClaveMoneda)
            .IsUnique()
            .HasDatabaseName("IX_CAT_Monedas_ClaveMoneda");
    }
}
