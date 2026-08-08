using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_PersonaPerfilConfiguration : IEntityTypeConfiguration<FI_PersonaPerfil>
{
    public void Configure(EntityTypeBuilder<FI_PersonaPerfil> builder)
    {
        builder.ToTable("FI_PersonaPerfil");

        builder.HasKey(e => new { e.PersonaId, e.PerfilId });

        builder.HasOne(e => e.FI_Persona)
            .WithMany(p => p.FI_PersonaPerfil)
            .HasForeignKey(e => e.PersonaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FI_Perfil)
            .WithMany(p => p.FI_PersonaPerfil)
            .HasForeignKey(e => e.PerfilId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
