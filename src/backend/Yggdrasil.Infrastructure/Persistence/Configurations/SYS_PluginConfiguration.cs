using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class SYS_PluginConfiguration : IEntityTypeConfiguration<SYS_Plugin>
{
    public void Configure(EntityTypeBuilder<SYS_Plugin> builder)
    {
        builder.ToTable("SYS_Plugins");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.PluginName)
            .IsRequired()
            .HasMaxLength(80)
            .HasColumnType("varchar(80)");

        builder.Property(e => e.PluginDescription)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnType("varchar(500)");

        builder.Property(e => e.MenuGlobal)
            .IsRequired();

        builder.Property(e => e.Active)
            .IsRequired();

        builder.HasOne(e => e.SYS_Application)
            .WithMany()
            .HasForeignKey(e => e.ApplicationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.ApplicationId)
            .HasDatabaseName("IX_SYS_Plugins_ApplicationId");

        builder.HasIndex(e => e.PluginName)
            .HasDatabaseName("IX_SYS_Plugins_PluginName");
    }
}
