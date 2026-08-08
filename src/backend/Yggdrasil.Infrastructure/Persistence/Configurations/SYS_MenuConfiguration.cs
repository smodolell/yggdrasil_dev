using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class SYS_MenuConfiguration : IEntityTypeConfiguration<SYS_Menu>
{
    public void Configure(EntityTypeBuilder<SYS_Menu> builder)
    {
        builder.ToTable("SYS_Menus");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(80)
            .HasColumnType("varchar(80)");

        builder.Property(e => e.Icon)
            .HasMaxLength(1000)
            .HasColumnType("varchar(1000)");

        builder.Property(e => e.Order)
            .IsRequired();

        builder.HasIndex(e => e.Name)
            .HasDatabaseName("IX_SYS_Menus_Name");
    }
}
