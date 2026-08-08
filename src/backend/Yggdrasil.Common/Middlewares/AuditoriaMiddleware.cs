using Ardalis.Result;
using LiteBus.Commands.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Reflection;
using Yggdrasil.Common.Interfaces;
using Yggdrasil.Common.Attributes;
using Yggdrasil.Common.Extensions;
using Yggdrasil.Domain.Entities;

namespace Yggdrasil.Common.Middlewares;

public class AuditoriaMiddleware<T>(IApplicationDbContext context, IUserContext userContext)
    : ICommandPostHandler<T> where T : ICommand
{
    private readonly IApplicationDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    // Caché estática para evitar AnyAsync repetitivos al catálogo de eventos
    private static readonly ConcurrentDictionary<int, bool> _eventCache = new();

    public async Task PostHandleAsync(T message, object? messageResult, CancellationToken cancellationToken = default)
    {
        var auditableAttr = typeof(T).GetCustomAttribute<AuditableAttribute>();
        if (auditableAttr == null) return;

        bool hasError = false;
        string messageDetail = $"Operación {typeof(T).Name} ejecutada.";

        // 1. Procesamiento de Resultado (Ardalis)
        if (messageResult is IResult result)
        {
            hasError = !(result.Status == ResultStatus.Ok || result.Status == ResultStatus.Created || result.Status == ResultStatus.NoContent);
            if (hasError)
            {
                var errores = result.Errors.Any()
                    ? string.Join(" | ", result.Errors)
                    : string.Join(" | ", result.ValidationErrors.Select(x => $"{x.Identifier}: {x.ErrorMessage}"));

                messageDetail = $"Fallo: {errores}";
            }
        }

        // 2. Asegurar existencia del Evento (Auto-sincronización del catálogo)
        await EnsureAuditEventExistsAsync(auditableAttr.EventId, cancellationToken);

        // 3. Registro de Auditoría
        var auditEntry = new SYS_Audit
        {
            Id = Guid.NewGuid(),
            AuditEventId = auditableAttr.EventId,
            RegisteredDate = DateTime.UtcNow,
            UserName = _userContext.UserName ?? "Sistema",
            HasError = hasError,
            Message = messageDetail
        };

        _context.SYS_Audit.Add(auditEntry);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureAuditEventExistsAsync(int eventId, CancellationToken ct)
    {
        // Si ya lo verificamos en esta sesión, no vamos a la DB
        if (_eventCache.ContainsKey(eventId)) return;

        var existeEnDb = await _context.SYS_AuditEvent.AnyAsync(x => x.Id == eventId, ct);

        if (!existeEnDb)
        {
            var eventEnum = (AuditEvents)eventId;
            _context.SYS_AuditEvent.Add(new SYS_AuditEvent
            {
                Id = eventId,
                Description = eventEnum.GetDescription() // Utiliza tu extensión GetDescription
            });
            await _context.SaveChangesAsync(ct);
        }

        _eventCache.TryAdd(eventId, true);
    }
}