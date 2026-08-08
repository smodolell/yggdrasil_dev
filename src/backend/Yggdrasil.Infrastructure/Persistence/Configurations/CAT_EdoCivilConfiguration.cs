using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Common.Constants;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class CAT_EdoCivilConfiguration : IEntityTypeConfiguration<CAT_EdoCivil>
{
    public void Configure(EntityTypeBuilder<CAT_EdoCivil> builder)
    {
        builder.ToTable("CAT_EdoCivil");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.NomEdoCivil)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnType("varchar(30)");

        builder.HasIndex(e => e.NomEdoCivil)
            .IsUnique()
            .HasDatabaseName("IX_CAT_EdosCivil_NomEdoCivil");

        builder.HasData(new CAT_EdoCivil { Id = AppConstants.CAT_EdoCivilId_Desconocido, NomEdoCivil = "Desconocido" });
        builder.HasData(new CAT_EdoCivil { Id = AppConstants.CAT_EdoCivilId_Soltero, NomEdoCivil = "Soltero" });
        builder.HasData(new CAT_EdoCivil { Id = AppConstants.CAT_EdoCivilId_Casado, NomEdoCivil = "Casado" });
        builder.HasData(new CAT_EdoCivil { Id = AppConstants.CAT_EdoCivilId_Viudo, NomEdoCivil = "Viudo" });
        builder.HasData(new CAT_EdoCivil { Id = AppConstants.CAT_EdoCivilId_Divorciado, NomEdoCivil = "Divorciado" });
        builder.HasData(new CAT_EdoCivil { Id = AppConstants.CAT_EdoCivilId_Union_Libre, NomEdoCivil = "Unión Libre" });
        builder.HasData(new CAT_EdoCivil { Id = AppConstants.CAT_EdoCivilId_Comprometido, NomEdoCivil = "Comprometido" });
    }
}
