using Microsoft.AspNetCore.Identity;

namespace Yggdrasil.Domain.Entities;

public class SYS_Rol : IdentityRole<int>
{
    public string? Descripcion { get; set; }

    public bool IsEnabled { get; set; }

    public SYS_Rol(string rolName) : base(rolName)
    {
    }

    public SYS_Rol()
    {
    }


    //public ICollection<RSP_ReporteRol> RSP_ReporteRol { get; set; } = new HashSet<RSP_ReporteRol>();


}

