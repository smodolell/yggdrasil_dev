using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_CargoInicialConfiguration : IEntityTypeConfiguration<FI_CargoInicial>
{
    public void Configure(EntityTypeBuilder<FI_CargoInicial> builder)
    {
        builder.ToTable("FI_CargoInicial");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Monto)
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.Iva)
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.Total)
            .HasColumnType("decimal(13, 2)");

        builder.HasOne(e => e.FI_Credito)
            .WithMany()
            .HasForeignKey(e => e.CreditoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FI_Cargo)
            .WithMany()
            .HasForeignKey(e => e.CargoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FI_TipoMovimiento)
            .WithMany()
            .HasForeignKey(e => e.TipoMovimientoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.CreditoId)
            .HasDatabaseName("IX_FI_CargoInicial_CreditoId");
    }
}
