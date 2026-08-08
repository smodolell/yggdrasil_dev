using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_MovimientoConfiguration : IEntityTypeConfiguration<FI_Movimiento>
{
    public void Configure(EntityTypeBuilder<FI_Movimiento> builder)
    {
        builder.ToTable("FI_Movimiento");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.DescMovimiento)
            .IsRequired()
            .HasMaxLength(80)
            .HasColumnType("varchar(80)");

        builder.Property(e => e.FechaRegistro)
            .IsRequired();

        builder.Property(e => e.FechaVencimiento)
            .IsRequired();

        builder.Property(e => e.Capital)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.Interes)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.Iva)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.Total)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.SaldoCapital)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.SaldoInteres)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.SaldoIva)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.SaldoTotal)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.HasOne(e => e.FI_Credito)
            .WithMany(c => c.FI_Movimiento)
            .HasForeignKey(e => e.CreditoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FI_TipoMovimiento)
            .WithMany()
            .HasForeignKey(e => e.TipoMovimientoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new { e.CreditoId, e.FechaVencimiento })
            .HasDatabaseName("IX_FI_Movimiento_CreditoId_FechaVencimiento");
    }
}
