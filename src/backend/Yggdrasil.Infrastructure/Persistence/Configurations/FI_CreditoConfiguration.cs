using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_CreditoConfiguration : IEntityTypeConfiguration<FI_Credito>
{
    public void Configure(EntityTypeBuilder<FI_Credito> builder)
    {
        builder.ToTable("FI_Credito");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.PersonaId)
            .HasColumnName("PersonaId")
            .IsRequired();

        builder.Property(e => e.FechaRegistro)
            .IsRequired();

        builder.Property(e => e.ClaveCredito)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnType("varchar(30)");

        builder.Property(e => e.Capital)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.CapitalFinanciado)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.Property(e => e.Tasa)
            .IsRequired()
            .HasColumnType("decimal(8, 4)");

        builder.Property(e => e.PuntosMas)
            .IsRequired()
            .HasColumnType("decimal(8, 4)");

        builder.Property(e => e.PuntosPor)
            .IsRequired()
            .HasColumnType("decimal(8, 4)");

        builder.Property(e => e.TasaBase)
            .IsRequired()
            .HasColumnType("decimal(8, 4)");

        builder.Property(e => e.TasaMora)
            .IsRequired()
            .HasColumnType("decimal(8, 4)");

        builder.Property(e => e.PuntosMasMora)
            .IsRequired()
            .HasColumnType("decimal(8, 4)");

        builder.Property(e => e.PuntosPorMora)
            .IsRequired()
            .HasColumnType("decimal(8, 4)");

        builder.Property(e => e.TasaBaseMora)
            .IsRequired()
            .HasColumnType("decimal(8, 4)");

        builder.Property(e => e.TasaIva)
            .IsRequired()
            .HasColumnType("decimal(8, 4)");

        builder.Property(e => e.PagoMensual)
            .IsRequired()
            .HasColumnType("decimal(13, 2)");

        builder.HasOne(e => e.FI_Persona)
            .WithMany(p => p.FI_Credito)
            .HasForeignKey(e => e.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FI_Producto)
            .WithMany()
            .HasForeignKey(e => e.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FI_EstatusCredito)
            .WithMany()
            .HasForeignKey(e => e.EstatusCreditoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.CAT_Moneda)
            .WithMany()
            .HasForeignKey(e => e.MonedaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.CAT_Periodicidad)
            .WithMany()
            .HasForeignKey(e => e.PeriodicidadId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.FI_TablaAmortiza)
            .WithOne(t => t.FI_Credito)
            .HasForeignKey(t => t.CreditoId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasIndex(e => e.ClaveCredito)
            .IsUnique()
            .HasDatabaseName("IX_FI_Credito_ClaveCredito");
    }
}
