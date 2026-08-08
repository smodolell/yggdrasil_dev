using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_PagoMovimientoConfiguration : IEntityTypeConfiguration<FI_PagoMovimiento>
{
    public void Configure(EntityTypeBuilder<FI_PagoMovimiento> builder)
    {
        builder.ToTable("FI_PagoMovimiento");

        builder.HasKey(e => new { e.PagoId, e.MovimientoId });

        builder.Property(e => e.TotalPagado)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.CapitalPagado)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.InteresPagado)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.IvaPagado)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.FechaPago)
            .IsRequired();

        builder.Property(e => e.Cancelado)
            .IsRequired();

        builder.Property(e => e.MotivoCancelacion)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(e => e.Activo)
            .IsRequired();

        builder.HasOne(e => e.FI_Pago)
            .WithMany(p => p.FI_PagoMovimiento)
            .HasForeignKey(e => e.PagoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FI_Movimiento)
            .WithMany(m => m.FI_PagoMovimiento)
            .HasForeignKey(e => e.MovimientoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
