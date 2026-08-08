using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class CAT_PeriodicidadConfiguration : IEntityTypeConfiguration<CAT_Periodicidad>
{
    public void Configure(EntityTypeBuilder<CAT_Periodicidad> builder)
    {
        builder.ToTable("CAT_Periodicidades");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.ClavePeriodicidad)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnType("varchar(10)");

        builder.Property(e => e.NomPeriodicidad)
            .IsRequired()
            .HasMaxLength(60)
            .HasColumnType("varchar(60)");

        builder.Property(e => e.ParamDias)
            .IsRequired();

        builder.Property(e => e.ParamMes)
            .IsRequired();

        builder.Property(e => e.NroPagosAnio)
            .IsRequired()
            .HasDefaultValue((short)0);

        builder.Property(e => e.NroPagosMes)
            .IsRequired()
            .HasDefaultValue((short)0);

        builder.HasIndex(e => e.ClavePeriodicidad)
            .IsUnique()
            .HasDatabaseName("IX_CAT_Periodicidades_ClavePeriodicidad");
    }
}
