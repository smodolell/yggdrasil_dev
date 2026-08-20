using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class CS_PagoMovimientoConfiguration : IEntityTypeConfiguration<CS_PagoMovimiento>
{
    public CS_PagoMovimientoConfiguration()
    {
    }

    public void Configure(EntityTypeBuilder<CS_PagoMovimiento> builder)
    {
        builder.ToTable("CS_PagoMovimiento");

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

        builder.HasOne(e => e.CS_Pago)
            .WithMany(p => p.CS_PagoMovimiento)
            .HasForeignKey(e => e.PagoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.CS_Movimiento)
            .WithMany(m => m.CS_PagoMovimiento)
            .HasForeignKey(e => e.MovimientoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
