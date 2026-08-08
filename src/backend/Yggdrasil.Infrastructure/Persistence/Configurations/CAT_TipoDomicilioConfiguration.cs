using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class CAT_TipoDomicilioConfiguration : IEntityTypeConfiguration<CAT_TipoDomicilio>
{
    public void Configure(EntityTypeBuilder<CAT_TipoDomicilio> builder)
    {
        builder.ToTable("CAT_TipoDomicilio");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.NomTipoDomicilio)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.HasIndex(e => e.NomTipoDomicilio)
            .IsUnique()
            .HasDatabaseName("IX_CAT_TiposDomicilio_NomTipoDomicilio");
    }
}
