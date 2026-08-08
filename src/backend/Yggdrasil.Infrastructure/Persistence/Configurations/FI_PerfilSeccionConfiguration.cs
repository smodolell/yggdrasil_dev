using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_PerfilSeccionConfiguration : IEntityTypeConfiguration<FI_PerfilSeccion>
{
    public void Configure(EntityTypeBuilder<FI_PerfilSeccion> builder)
    {
        builder.ToTable("FI_PerfilSeccion");

        builder.HasKey(e => new { e.PerfilId, e.SeccionId });

        builder.Property(e => e.ActivoCreate)
            .HasDefaultValue(false);

        builder.Property(e => e.ActivoEdit)
            .HasDefaultValue(false);

        builder.Property(e => e.ActivoExtension)
            .HasDefaultValue(false);

        builder.HasOne(e => e.FI_Perfil)
            .WithMany(p => p.FI_PerfilSeccion)
            .HasForeignKey(e => e.PerfilId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FI_Seccion)
            .WithMany(s => s.FI_PerfilSeccion)
            .HasForeignKey(e => e.SeccionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
