using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_SeccionConfiguration : IEntityTypeConfiguration<FI_Seccion>
{
    public void Configure(EntityTypeBuilder<FI_Seccion> builder)
    {
        builder.ToTable("FI_Seccion");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.NomSeccion)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("varchar(200)");

        builder.Property(e => e.Orden)
            .IsRequired();

        builder.Property(e => e.IsCreate)
            .HasDefaultValue(false);

        builder.Property(e => e.IsEdit)
            .HasDefaultValue(false);

        builder.Property(e => e.IsExtension)
            .HasDefaultValue(false);
    }
}
