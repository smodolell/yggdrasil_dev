using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Common.Constants;
using Yggdrasil.Domain.Entities;

public class CS_EstatusCreditoConfiguration : IEntityTypeConfiguration<CS_EstatusCredito>
{
    public void Configure(EntityTypeBuilder<CS_EstatusCredito> builder)
    {
        builder.ToTable("CS_EstatusCredito");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.NomEstatusCredito)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnType("varchar(30)");

        builder.HasData(new CS_EstatusCredito { Id = AppConstants.CAT_EstatusCreditoId_ACTIVO, NomEstatusCredito = "ACTIVO" });
        builder.HasData(new CS_EstatusCredito { Id = AppConstants.CAT_EstatusCreditoId_CAPTURADO, NomEstatusCredito = "CAPTURADO" });
        builder.HasData(new CS_EstatusCredito { Id = AppConstants.CAT_EstatusCreditoId_TERMINADO, NomEstatusCredito = "TERMINADO" });
    }
}
