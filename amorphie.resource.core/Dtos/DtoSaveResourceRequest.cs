using amorphie.core.Base;
using amorphie.core.Enums;

public class DtoSaveResourceRequest
{
    public Guid? Id { get; set; }
    public ICollection<MultilanguageText> DisplayNames { get; set; } = default!;
    public HttpMethodType Type { get; set; }
    public string? Url { get; set; }
    public ICollection<MultilanguageText> Descriptions { get; set; } = default!;
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? CreatedByBehalfOf { get; set; }
    public Guid ModifiedBy { get; set; }
    public Guid? ModifiedByBehalfOf { get; set; }
}