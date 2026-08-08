using Yggdrasil.Common.Models.StoredProcedures;

namespace Yggdrasil.Common.Interfaces;

public interface IApplicationDbContextProcedures
{

    Task<int> usp_CreateCalendarioLaboralAsync(int? anio = null, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
}
