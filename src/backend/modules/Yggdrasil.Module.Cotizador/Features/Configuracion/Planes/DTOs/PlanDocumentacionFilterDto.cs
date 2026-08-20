//namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;
//public class PlanDocumentacionFilterDto : Specification<OT_PlanDocumentacion>
//{

//    public int PlanId { get; set; }
//    public int? TipoPersonaId { get; set; }
//    public int? TipoDocumentacionId { get; set; }
//    public string? SearchText { get; set; }


//    public bool Activo { get; set; }
//    public override Expression<Func<OT_PlanDocumentacion, bool>> ToExpression()
//    {
//        var predicate = PredicateBuilder.New<OT_PlanDocumentacion>(true);

//        predicate.And(r => r.PlanId == PlanId);

//        if (!string.IsNullOrEmpty(SearchText))
//        {
//            predicate.And(r => r.EXP_Documentacion.NomDocumentacion.Contains(SearchText));
//        }
//        if (TipoPersonaId != null)
//        {
//            predicate.And(r => r.TipoPersonaId == TipoPersonaId.Value);
//        }
//        if (TipoDocumentacionId != null)
//        {
//            predicate.And(r => r.EXP_Documentacion.TipoDocumentacionId == TipoDocumentacionId.Value);
//        }

//        return predicate;
//    }
//}
