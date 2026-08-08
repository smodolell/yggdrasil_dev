using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class CAT_TasaIvaConfiguration : IEntityTypeConfiguration<CAT_TasaIva>
{
    public void Configure(EntityTypeBuilder<CAT_TasaIva> builder)
    {
        builder.ToTable("CAT_TasasIva");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.ValorTasa)
            .IsRequired()
            .HasColumnType("decimal(8,4)");

        builder.Property(e => e.NomTasaIva)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnType("varchar(30)");

        builder.HasIndex(e => e.NomTasaIva)
            .HasDatabaseName("IX_CAT_TasasIva_NomTasaIva");
    }
}
