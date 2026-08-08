using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class SYS_AuditEventConfiguration : IEntityTypeConfiguration<SYS_AuditEvent>
{
    public void Configure(EntityTypeBuilder<SYS_AuditEvent> builder)
    {
        builder.ToTable("SYS_AuditEvents");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.HasIndex(e => e.Description)
            .IsUnique()
            .HasDatabaseName("IX_SYS_AuditEvents_Description");
    }
}
