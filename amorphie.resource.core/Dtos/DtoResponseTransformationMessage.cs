using amorphie.core.Base;

public class DtoResponseTransformationMessage : DtoBase
{
    public Guid ResponseTransformationId { get; set; }
    public string? Language { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Icon { get; set; }
}

public class DtoGetResponseTransformationMessage
{
    public Guid ResponseTransformationId { get; set; }
    public string? Language { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Icon { get; set; }
}