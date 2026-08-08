using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class SYS_RolConfiguration : IEntityTypeConfiguration<SYS_Rol>
{
    public void Configure(EntityTypeBuilder<SYS_Rol> builder)
    {
        // SYS_Rol extends IdentityRole<int> which maps to AspNetRoles table
        // Only configure the additional properties beyond IdentityRole<int>

        builder.Property(e => e.Descripcion)
            .HasMaxLength(500)
            .HasColumnType("nvarchar(500)");

        builder.Property(e => e.IsEnabled)
            .IsRequired();
    }
}
