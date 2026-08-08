namespace Yggdrasil.Module.Catalog.Features.SelectLists.Queries;

public abstract class SelectListQueryBase : IQuery<Result<List<SelectListItemDto>>>
{
    public string? SearchTerm { get; set; }
    public int? MaxResults { get; set; }
}

