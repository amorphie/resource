using amorphie.core.Base;
using amorphie.core.Enums;

public class Resource : EntityBase
{
    public ICollection<Translation> DisplayNames { get; set; } = default!;
    public HttpMethodType Type { get; set; }
    public string? Url { get; set; }
    public ICollection<Translation> Descriptions { get; set; } = default!;
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
}