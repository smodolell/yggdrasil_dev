#nullable disable
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Yggdrasil.Common.Interfaces;
using Yggdrasil.Common.Models.StoredProcedures;

namespace Yggdrasil.Infrastructure.Persistence;

public partial class ApplicationDbContext
{
    private IApplicationDbContextProcedures _procedures;
    public virtual IApplicationDbContextProcedures Procedures
    {
        get
        {
            if (_procedures is null) _procedures = new ApplicationDbContextProcedures(this);
            return _procedures;
        }
        set
        {
            _procedures = value;
        }
    }

    public IApplicationDbContextProcedures GetProcedures()
    {
        return Procedures;
    }
}

public partial class ApplicationDbContextProcedures : IApplicationDbContextProcedures
{
    private readonly ApplicationDbContext _context;
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
    public ApplicationDbContextProcedures(ApplicationDbContext context)
    {
        _context = context;
    }
    // Constructor optimizado para soportar llamadas en paralelo libres de hilos
    public ApplicationDbContextProcedures(ApplicationDbContext context, IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _context = context;
        _contextFactory = contextFactory;
    }


    public virtual async Task<int> usp_CreateCalendarioLaboralAsync(int? anio = null, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
    {
        var parameterreturnValue = new SqlParameter
        {
            ParameterName = "returnValue",
            Direction = System.Data.ParameterDirection.Output,
            SqlDbType = System.Data.SqlDbType.Int,
        };

        var sqlParameters = new[]
        {
                new SqlParameter
                {
                    ParameterName = "Anio",
                    Value = anio ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                parameterreturnValue,
            };
        var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[usp_CreateCalendarioLaboral] @Anio = @Anio", sqlParameters, cancellationToken);

        returnValue?.SetValue(parameterreturnValue.Value);

        return _;
    }
}
