using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class CAT_EmpresaConfiguration : IEntityTypeConfiguration<CAT_Empresa>
{
    public void Configure(EntityTypeBuilder<CAT_Empresa> builder)
    {
        builder.ToTable("CAT_Empresas");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.NomEmpresa)
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.HasIndex(e => e.NomEmpresa)
            .HasDatabaseName("IX_CAT_Empresas_NomEmpresa");
    }
}
