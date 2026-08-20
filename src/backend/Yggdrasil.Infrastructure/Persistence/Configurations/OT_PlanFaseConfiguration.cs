using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

class OT_PlanFaseConfiguration : IEntityTypeConfiguration<OT_PlanFase>
{
    public void Configure(EntityTypeBuilder<OT_PlanFase> builder)
    {
        builder.HasKey(bc => new { bc.PlanId, bc.FaseId });
        builder.HasOne(bc => bc.OT_Plan).WithMany(b => b.OT_PlanFase).HasForeignKey(bc => bc.PlanId);
        builder.HasOne(bc => bc.OT_Fase).WithMany(b => b.OT_PlanFase).HasForeignKey(bc => bc.FaseId);
    }
}