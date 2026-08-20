//using DocumentFormat.OpenXml.InkML;

//namespace Yggdrasil.Module.Cobranza.Features.Intradias.Commands;

//public record CierreCommand(Guid CreditoId, DateTime FechaCierre) : ICommand<Result>;
//internal class CierreCommandHandler(IApplicationDbContext context, ICommandMediator commandMediator) : ICommandHandler<CierreCommand, Result>
//{
//    public async Task<Result> HandleAsync(CierreCommand message, CancellationToken cancellationToken = default)
//    {

//        var credito = await context.DEV_CreditoIntraDia
//            .SingleOrDefaultAsync(c => c.Id == message.CreditoId, cancellationToken);

//        if (credito == null)
//            return Result.NotFound("Crédito no encontrado");


//        var command = new DevengarCommand(message.CreditoId, message.FechaCierre);
//        var result = await commandMediator.SendAsync(command);
//        if (result.IsSuccess)
//        {

//            var interesAcumulado = await context.DEV_InteresAcumulado
//                .SingleOrDefaultAsync(i => i.CreditoId == message.CreditoId, cancellationToken);

//        }
//    }
//}