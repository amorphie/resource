using amorphie.core.Base;

public class ResponseTransformation : EntityBase
{
    public string? ResponseCode { get; set; }
    public string? Filter { get; set; }
    public string[]? Audience { get; set; }
    public string? DisplayMode { get; set; }
    public ICollection<ResponseTransformationMessage> ResponseTransformationMessages { get; set; } = default!;
}