using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_DomicilioConfiguration : IEntityTypeConfiguration<FI_Domicilio>
{
    public void Configure(EntityTypeBuilder<FI_Domicilio> builder)
    {
        builder.ToTable("FI_Domicilio");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.FechaRegistro)
            .IsRequired();

        builder.Property(e => e.Calle)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(e => e.Numero)
            .HasMaxLength(30)
            .HasColumnType("varchar(30)");

        builder.Property(e => e.Piso)
            .HasMaxLength(10)
            .HasColumnType("varchar(10)");

        builder.Property(e => e.EntreCalles)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(e => e.YCalle)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");
    }
}
