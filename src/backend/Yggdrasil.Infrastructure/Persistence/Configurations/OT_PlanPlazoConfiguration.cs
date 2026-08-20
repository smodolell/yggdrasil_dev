using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

class OT_PlanPlazoConfiguration : IEntityTypeConfiguration<OT_PlanPlazo>
{
    public void Configure(EntityTypeBuilder<OT_PlanPlazo> builder)
    {
        builder.HasKey(bc => new { bc.PlanId, bc.ValorPlazo });
        builder.HasOne(bc => bc.OT_Plan).WithMany(b => b.OT_PlanPlazo).HasForeignKey(bc => bc.PlanId);
        builder.HasOne(bc => bc.CAT_Plazo).WithMany().HasForeignKey(bc => bc.PlazoId);
        builder.HasOne(bc => bc.CAT_Tasa).WithMany().HasForeignKey(bc => bc.TasaId);
    }
}