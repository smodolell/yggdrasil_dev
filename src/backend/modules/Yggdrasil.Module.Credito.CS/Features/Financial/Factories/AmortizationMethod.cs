using System.ComponentModel;

namespace Yggdrasil.Module.Credito.CS.Features.Financial.Factories;

public enum AmortizationMethod
{
    [Description("Sistema Francés (Cuota fija)")]
    French = 1,
    [Description("Sistema Alemán (Capital constante)")]
    German = 2,
    [Description("Sistema Americano (Bullet)")]
    American = 3,
    [Description("Amortización BBVA")]
    BBVA = 4,
    [Description("Importar desde Excel")]
    ImportExcel = 5,
}

