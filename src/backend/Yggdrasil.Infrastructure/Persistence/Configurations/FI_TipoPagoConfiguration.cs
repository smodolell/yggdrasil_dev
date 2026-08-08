using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_TipoPagoConfiguration : IEntityTypeConfiguration<FI_TipoPago>
{
    public void Configure(EntityTypeBuilder<FI_TipoPago> builder)
    {
        builder.ToTable("CAT_TipoPago");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.NomTipoPago)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");
    }
}
