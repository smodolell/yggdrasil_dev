using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_PerfilConfiguration : IEntityTypeConfiguration<FI_Perfil>
{
    public void Configure(EntityTypeBuilder<FI_Perfil> builder)
    {
        builder.ToTable("FI_Perfil");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.NomPerfil)
            .IsRequired()
            .HasMaxLength(80)
            .HasColumnType("varchar(80)");

        builder.Property(e => e.Activo)
            .HasDefaultValue(false);
    }
}
