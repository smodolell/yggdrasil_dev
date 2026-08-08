using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_CargoConfiguration : IEntityTypeConfiguration<FI_Cargo>
{
    public void Configure(EntityTypeBuilder<FI_Cargo> builder)
    {
        builder.ToTable("FI_Cargo");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Concepto)
            .IsRequired()
            .HasMaxLength(80)
            .HasColumnType("varchar(80)");

        builder.Property(e => e.Monto)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.Porcentaje)
            .IsRequired()
            .HasColumnType("decimal(8, 2)");

        builder.Property(e => e.EsCargoInicial)
            .HasDefaultValue(false);

        builder.Property(e => e.EsConceptoFinanciado)
            .HasDefaultValue(false);

        builder.Property(e => e.PermiteEdicion)
            .HasDefaultValue(false);

        builder.Property(e => e.Activo)
            .HasDefaultValue(false);

        builder.HasOne(e => e.FI_Producto)
            .WithMany(p => p.FI_Cargo)
            .HasForeignKey(e => e.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FI_TipoMovimiento)
            .WithMany()
            .HasForeignKey(e => e.TipoMovimientoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FI_TipoCalculo)
            .WithMany()
            .HasForeignKey(e => e.TipoCalculoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FI_FormaPago)
            .WithMany()
            .HasForeignKey(e => e.FormaPagoId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
