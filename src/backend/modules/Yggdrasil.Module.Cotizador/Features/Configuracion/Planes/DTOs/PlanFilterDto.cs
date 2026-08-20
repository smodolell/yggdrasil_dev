//namespace Yggdrasil.Module.Otorgamiento.Services.Plan.Dtos;

//public class PlanFilterDto : Specification<OT_Plan>
//{
//    public string? SearchText { get; set; }
//    public int? ProductoId { get; set; }
//    public decimal? ValMin { get; set; }
//    public decimal? ValMax { get; set; }

//    public override Expression<Func<OT_Plan, bool>> ToExpression()
//    {
//        var predicate = PredicateBuilder.New<OT_Plan>(true);

//        if (!string.IsNullOrEmpty(SearchText))
//        {
//            predicate.And(p => p.NomPlan.Contains(SearchText));
//        }

//        if (ProductoId != null && ProductoId.HasValue)
//        {
//            predicate.And(p => p.ProductoId == ProductoId.Value);
//        }

//        if (ValMin != null)
//        {
//            predicate.And(p => p.ImporteMinimo >= ValMin.Value);
//        }

//        if (ValMax != null)
//        {
//            predicate.And(p => p.ImporteMaximo <= ValMax.Value);
//        }
//        return predicate;
//    }
//}