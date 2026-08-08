using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class CAT_PlazoConfiguration : IEntityTypeConfiguration<CAT_Plazo>
{
    public void Configure(EntityTypeBuilder<CAT_Plazo> builder)
    {
        builder.ToTable("CAT_Plazos");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.ValorPlazo)
            .IsRequired();

        builder.HasIndex(e => e.ValorPlazo)
            .IsUnique()
            .HasDatabaseName("IX_CAT_Plazos_ValorPlazo");
    }
}
