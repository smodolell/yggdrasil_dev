using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Common.Constants;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class CAT_GeneroConfiguration : IEntityTypeConfiguration<CAT_Genero>
{
    public void Configure(EntityTypeBuilder<CAT_Genero> builder)
    {
        builder.ToTable("CAT_Generos");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.NomGenero)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnType("varchar(30)");

        builder.HasIndex(e => e.NomGenero)
            .IsUnique()
            .HasDatabaseName("IX_CAT_Generos_NomGenero");

        builder.HasData(new CAT_Genero { Id = AppConstants.CAT_GeneroId_MASCULINO, NomGenero = "MASCULINO" });
        builder.HasData(new CAT_Genero { Id = AppConstants.CAT_GeneroId_FEMENINO, NomGenero = "FEMENINO" });

    }
}
