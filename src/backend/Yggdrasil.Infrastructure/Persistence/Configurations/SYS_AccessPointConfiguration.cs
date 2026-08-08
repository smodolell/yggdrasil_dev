using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class SYS_AccessPointConfiguration : IEntityTypeConfiguration<SYS_AccessPoint>
{
    public void Configure(EntityTypeBuilder<SYS_AccessPoint> builder)
    {
        builder.ToTable("SYS_AccessPoints");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.AccessPointName)
            .IsRequired();

        builder.Property(e => e.Icon)
            .HasMaxLength(500)
            .HasColumnType("varchar(500)");

        builder.Property(e => e.Route)
            .IsRequired();

        builder.Property(e => e.DescPageElement)
            .IsRequired();

        builder.Property(e => e.Order)
            .IsRequired();

        builder.Property(e => e.IsAnonymous)
            .IsRequired();

        builder.HasOne(e => e.SYS_Menu)
            .WithMany(m => m.SYS_AccessPoint)
            .HasForeignKey(e => e.MenuId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.SYS_Plugin)
            .WithMany(p => p.SYS_AccessPoint)
            .HasForeignKey(e => e.PluginId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.SYS_AccessPointType)
            .WithMany()
            .HasForeignKey(e => e.AccessPointTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.MenuId)
            .HasDatabaseName("IX_SYS_AccessPoints_MenuId");

        builder.HasIndex(e => e.PluginId)
            .HasDatabaseName("IX_SYS_AccessPoints_PluginId");

        builder.HasIndex(e => e.AccessPointTypeId)
            .HasDatabaseName("IX_SYS_AccessPoints_AccessPointTypeId");

        builder.HasIndex(e => e.ApplicationId)
            .HasDatabaseName("IX_SYS_AccessPoints_ApplicationId");
    }
}
