using System.Text.Json.Serialization;
using amorphie.core.Base;

public class DtoResponseTransformation : DtoBase
{
    public string? ResponseCode { get; set; }
    public string? Filter { get; set; }
    public string[]? Audience { get; set; }
    public string? DisplayMode { get; set; }
    public ICollection<DtoResponseTransformationMessage> ResponseTransformationMessages { get; set; } = default!;
}

public class DtoGetResponseTransformation
{
    [JsonPropertyName("response-code")]
    public string? ResponseCode { get; set; }

    public string? Filter { get; set; }
    public string[]? Audience { get; set; }

    [JsonPropertyName("display-mode")]
    public string? DisplayMode { get; set; }

    [JsonPropertyName("messages")]
    public ICollection<DtoGetResponseTransformationMessage> ResponseTransformationMessages { get; set; } = default!;
}