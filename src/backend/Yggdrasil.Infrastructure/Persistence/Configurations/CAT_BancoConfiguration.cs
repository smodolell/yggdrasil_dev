using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;


namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class CAT_BancoConfiguration : IEntityTypeConfiguration<CAT_Banco>
{
    public void Configure(EntityTypeBuilder<CAT_Banco> builder)
    {
        builder.ToTable("CAT_Banco");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.NomBanco)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnType("varchar(30)");

        builder.Property(e => e.CBUPrefix)
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnType("varchar(3)")
            .IsFixedLength();

        builder.Property(e => e.CodigoBCRA)
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnType("varchar(3)")
            .IsFixedLength();

        builder.HasIndex(e => e.NomBanco)
            .HasDatabaseName("IX_CAT_Bancos_NomBanco");

        builder.HasIndex(e => e.CBUPrefix)
            .HasDatabaseName("IX_CAT_Bancos_CBUPrefix");

        builder.HasIndex(e => e.CodigoBCRA)
            .IsUnique()
            .HasDatabaseName("IX_CAT_Bancos_CodigoBCRA");
    }
}

