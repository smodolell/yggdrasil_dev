using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class SYS_AccessPointTypeConfiguration : IEntityTypeConfiguration<SYS_AccessPointType>
{
    public void Configure(EntityTypeBuilder<SYS_AccessPointType> builder)
    {
        builder.ToTable("SYS_AccessPointTypes");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.AccessPointTypeName)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnType("varchar(30)");

        builder.HasIndex(e => e.AccessPointTypeName)
            .IsUnique()
            .HasDatabaseName("IX_SYS_AccessPointTypes_AccessPointTypeName");


        builder.HasData(
           new SYS_AccessPointType { Id = 0, AccessPointTypeName = "LeftMenu" },
           new SYS_AccessPointType { Id = 1, AccessPointTypeName = "Page" },
           new SYS_AccessPointType { Id = 2, AccessPointTypeName = "Element" }
       );
    }
}
