using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Common.Constants;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

public class FI_EstatusCreditoConfiguration : IEntityTypeConfiguration<FI_EstatusCredito>
{
    public void Configure(EntityTypeBuilder<FI_EstatusCredito> builder)
    {
        builder.ToTable("FI_EstatusCredito");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.NomEstatusCredito)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnType("varchar(30)");

        builder.HasData(new FI_EstatusCredito { Id = AppConstants.CAT_EstatusCreditoId_ACTIVO, NomEstatusCredito = "ACTIVO" });
        builder.HasData(new FI_EstatusCredito { Id = AppConstants.CAT_EstatusCreditoId_CAPTURADO, NomEstatusCredito = "CAPTURADO" });
        builder.HasData(new FI_EstatusCredito { Id = AppConstants.CAT_EstatusCreditoId_TERMINADO, NomEstatusCredito = "TERMINADO" });
    }
}
