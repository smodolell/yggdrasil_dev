using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_ConceptoFinanciadoConfiguration : IEntityTypeConfiguration<FI_ConceptoFinanciado>
{
    public void Configure(EntityTypeBuilder<FI_ConceptoFinanciado> builder)
    {
        builder.ToTable("FI_ConceptoFinanciado");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Monto)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.Iva)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.Total)
            .IsRequired()
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
            .HasDatabaseName("IX_FI_ConceptoFinanciado_CreditoId");
    }
}
