using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Common.Constants;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class CAT_TipoPersonaConfiguration : IEntityTypeConfiguration<CAT_TipoPersona>
{
    public void Configure(EntityTypeBuilder<CAT_TipoPersona> builder)
    {
        builder.ToTable("CAT_TipoPersona");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.NomTipoPersona)
            .IsRequired()
            .HasMaxLength(40)
            .HasColumnType("varchar(40)");

        builder.Property(e => e.Activo)
            .IsRequired();

        builder.HasIndex(e => e.NomTipoPersona)
            .IsUnique()
            .HasDatabaseName("IX_CAT_TiposPersona_NomTipoPersona");

        builder.HasData(new CAT_TipoPersona { Id = AppConstants.CAT_TipoPersonaId_PersonaFisica, NomTipoPersona = "Persona Física" });
        builder.HasData(new CAT_TipoPersona { Id = AppConstants.CAT_TipoPersonaId_PersonaJuridia, NomTipoPersona = "Persona Moral" });
    }
}
