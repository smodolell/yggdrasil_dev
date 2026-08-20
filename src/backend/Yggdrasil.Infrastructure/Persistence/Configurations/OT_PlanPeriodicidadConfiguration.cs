using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

class OT_PlanPeriodicidadConfiguration : IEntityTypeConfiguration<OT_PlanPeriodicidad>
{
    public void Configure(EntityTypeBuilder<OT_PlanPeriodicidad> builder)
    {
        builder.HasKey(bc => new { bc.PlanId, bc.PeriodicidadId });
        builder.HasOne(bc => bc.OT_Plan).WithMany(b => b.OT_PlanPeriodicidad).HasForeignKey(bc => bc.PlanId);
        builder.HasOne(bc => bc.CAT_Periodicidad).WithMany().HasForeignKey(bc => bc.PeriodicidadId);
    }
}
