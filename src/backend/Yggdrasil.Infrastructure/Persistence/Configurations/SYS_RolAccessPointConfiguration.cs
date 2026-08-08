using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class SYS_RolAccessPointConfiguration : IEntityTypeConfiguration<SYS_RolAccessPoint>
{
    public void Configure(EntityTypeBuilder<SYS_RolAccessPoint> builder)
    {
        builder.ToTable("SYS_RolAccessPoints");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.SYS_Rol)
            .WithMany()
            .HasForeignKey(e => e.RolId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.SYS_AccessPoint)
            .WithMany(a => a.SYS_RolAccessPoint)
            .HasForeignKey(e => e.AccessPointId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.RolId, e.AccessPointId })
            .IsUnique()
            .HasDatabaseName("IX_SYS_RolAccessPoints_RolId_AccessPointId");

        builder.HasIndex(e => e.AccessPointId)
            .HasDatabaseName("IX_SYS_RolAccessPoints_AccessPointId");
    }
}
