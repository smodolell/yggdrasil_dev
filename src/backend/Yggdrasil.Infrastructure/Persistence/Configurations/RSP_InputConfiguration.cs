using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Infrastructure.Persistence.Configurations;

internal class RSP_InputConfiguration : IEntityTypeConfiguration<RSP_Input>
{
    public void Configure(EntityTypeBuilder<RSP_Input> builder)
    {
        builder.HasData(new RSP_Input { Id = 1, NomInput = "TextBox" });
        builder.HasData(new RSP_Input { Id = 2, NomInput = "CheckBox" });
        builder.HasData(new RSP_Input { Id = 3, NomInput = "TextBoxDatepicker" });
        builder.HasData(new RSP_Input { Id = 4, NomInput = "DropDownList" });
    }
}

