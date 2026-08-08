using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class SYS_ApplicationConfiguration : IEntityTypeConfiguration<SYS_Application>
{
    public void Configure(EntityTypeBuilder<SYS_Application> builder)
    {
        builder.ToTable("SYS_Applications");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.ApplicationName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.HasIndex(e => e.ApplicationName)
            .IsUnique()
            .HasDatabaseName("IX_SYS_Applications_ApplicationName");
    }
}
