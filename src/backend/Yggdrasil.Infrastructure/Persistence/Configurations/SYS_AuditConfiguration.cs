using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class SYS_AuditConfiguration : IEntityTypeConfiguration<SYS_Audit>
{
    public void Configure(EntityTypeBuilder<SYS_Audit> builder)
    {
        builder.ToTable("SYS_Audits");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.RegisteredDate)
            .IsRequired();

        builder.Property(e => e.UserName)
            .IsRequired()
            .HasMaxLength(60)
            .HasColumnType("varchar(60)");

        builder.Property(e => e.HasError)
            .IsRequired();

        builder.Property(e => e.Message)
            .IsRequired();

        builder.HasOne(e => e.SYS_AuditEvent)
            .WithMany()
            .HasForeignKey(e => e.AuditEventId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(e => e.AuditEventId)
            .HasDatabaseName("IX_SYS_Audits_AuditEventId");

        builder.HasIndex(e => e.RegisteredDate)
            .HasDatabaseName("IX_SYS_Audits_RegisteredDate");

        builder.HasIndex(e => e.UserName)
            .HasDatabaseName("IX_SYS_Audits_UserName");
    }
}
