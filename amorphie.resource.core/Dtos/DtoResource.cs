using amorphie.core.Base;
using amorphie.core.Enums;

public class DtoResource : DtoBase
{
    public ICollection<MultilanguageText> DisplayNames { get; set; } = default!;
    public HttpMethodType Type { get; set; }
    public string? Url { get; set; }
    public ICollection<MultilanguageText> Descriptions { get; set; } = default!;
    public string[]? Tags { get; set; }
    public Guid? ResourceGroupId { get; set; }
    public string? Status { get; set; }
}