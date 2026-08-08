using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_CuentaBancariaConfiguration : IEntityTypeConfiguration<FI_CuentaBancaria>
{
    public void Configure(EntityTypeBuilder<FI_CuentaBancaria> builder)
    {
        builder.ToTable("FI_CuentaBancaria");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.NroCuentaBancaria)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(e => e.CBU)
            .HasMaxLength(30)
            .HasColumnType("varchar(30)");

        builder.Property(e => e.AliasCBU)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.HasOne(e => e.CAT_Banco)
            .WithMany()
            .HasForeignKey(e => e.BancoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.CAT_Moneda)
            .WithMany()
            .HasForeignKey(e => e.MonedaId)
            .OnDelete(DeleteBehavior.Restrict);



        builder.HasIndex(e => e.BancoId)
            .HasDatabaseName("IX_FI_CuentaBancaria_BancoId");

        builder.HasIndex(e => e.MonedaId)
            .HasDatabaseName("IX_FI_CuentaBancaria_MonedaId");
    }
}
