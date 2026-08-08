using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_PagoConfiguration : IEntityTypeConfiguration<FI_Pago>
{
    public void Configure(EntityTypeBuilder<FI_Pago> builder)
    {
        builder.ToTable("FI_Pago");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.FechaRegistro)
            .IsRequired();

        builder.Property(e => e.FechaPago)
            .IsRequired();

        builder.Property(e => e.FechaModificacion)
            .IsRequired();

        builder.Property(e => e.Monto)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.SaldoFavor)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.Cancelado)
            .IsRequired();

        builder.Property(e => e.Suspenso)
            .IsRequired();

        builder.Property(e => e.Activo)
            .IsRequired();
    }
}
