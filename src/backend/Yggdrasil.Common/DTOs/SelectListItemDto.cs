namespace Yggdrasil.Common.DTOs;

public class SelectListItemDto
{
    public string Text { get; set; } = "";
    public string Value { get; set; } = "";
    public decimal ValueDecimal { get; set; } = 0;
    public bool Selected { get; set; }
}
