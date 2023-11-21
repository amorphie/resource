using System.Text.Json.Serialization;
using amorphie.core.Base;

public class DtoResponseTransformation : DtoBase
{
    public string? ResponseCode { get; set; }
    public string? Filter { get; set; }
    public string[]? Audience { get; set; }
    public string? DisplayMode { get; set; }
    public List<DtoResponseTransformationMessage> ResponseTransformationMessages { get; set; } = default!;
}

public class DtoGetResponseTransformation
{
    [JsonPropertyName("display-mode")]
    public string? DisplayMode { get; set; }

    [JsonIgnore]
    public List<DtoGetResponseTransformationMessage> ResponseTransformationMessages { get; set; } = default!;

    public string? Title
    {
        get
        {
            if (ResponseTransformationMessages != null && ResponseTransformationMessages.Count > 0)
            {
                return ResponseTransformationMessages[0].Title;
            }

            return "";
        }
    }

    public string? Subtitle
    {
        get
        {
            if (ResponseTransformationMessages != null && ResponseTransformationMessages.Count > 0)
            {
                return ResponseTransformationMessages[0].Subtitle;
            }

            return "";
        }
    }

    public string? Icon
    {
        get
        {
            if (ResponseTransformationMessages != null && ResponseTransformationMessages.Count > 0)
            {
                return ResponseTransformationMessages[0].Icon;
            }

            return "";
        }
    }
}

public class DtoGetResponseTransformationRequest
{
    public string? ResponseCode { get; set; }
    public string? Audience { get; set; }
    public string? Language { get; set; }
    public string? Body { get; set; }
}