using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class SYS_UsuarioConfiguration : IEntityTypeConfiguration<SYS_Usuario>
{
    public void Configure(EntityTypeBuilder<SYS_Usuario> builder)
    {
        // SYS_Usuario extends IdentityUser<int> which maps to AspNetUsers table (via [Table] attribute)
        // Only configure the additional properties beyond IdentityUser<int>

        builder.Property(e => e.FechaRegistro)
            .IsRequired();

        builder.Property(e => e.NombreCompleto)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("nvarchar(200)");

        builder.Property(e => e.Telefono)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(e => e.Avatar)
            .HasMaxLength(200)
            .HasColumnType("nvarchar(200)");

        builder.Property(e => e.IsEnabled)
            .IsRequired();

        builder.Property(e => e.IsDeleted)
            .IsRequired();

        builder.Property(e => e.IsSpecial)
            .IsRequired();

        builder.HasIndex(e => e.NombreCompleto)
            .HasDatabaseName("IX_AspNetUsers_NombreCompleto");
    }
}
