using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

class OT_SolicitudFaseConfiguration : IEntityTypeConfiguration<OT_SolicitudFase>
{
    public void Configure(EntityTypeBuilder<OT_SolicitudFase> builder)
    {
        builder.HasKey(bc => new { bc.FaseId, bc.SolicitudId });
        builder.HasOne(bc => bc.OT_Solicitud).WithMany(b => b.OT_SolicitudFase).HasForeignKey(bc => bc.SolicitudId);
        builder.HasOne(bc => bc.OT_Fase).WithMany(c => c.OT_SolicitudFase).HasForeignKey(bc => bc.FaseId);
    }
}
