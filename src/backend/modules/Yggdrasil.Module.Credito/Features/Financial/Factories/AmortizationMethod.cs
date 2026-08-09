using System.ComponentModel;

namespace Yggdrasil.Module.Credito.Features.Financial.Factories;

public enum AmortizationMethod
{
    [Description("Sistema Francés (Cuota fija)")]
    French = 1,
    [Description("Sistema Alemán (Capital constante)")]
    German = 2,
    [Description("Sistema Americano (Bullet)")]
    American = 3,
}

