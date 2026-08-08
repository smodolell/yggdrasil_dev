using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class CAT_TipoTelefonoConfiguration : IEntityTypeConfiguration<CAT_TipoTelefono>
{
    public void Configure(EntityTypeBuilder<CAT_TipoTelefono> builder)
    {
        builder.ToTable("CAT_TiposTelefono");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.NomTipoTelefono)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.HasIndex(e => e.NomTipoTelefono)
            .IsUnique()
            .HasDatabaseName("IX_CAT_TiposTelefono_NomTipoTelefono");
    }
}
