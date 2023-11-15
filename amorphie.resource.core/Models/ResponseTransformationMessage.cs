using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class ResponseTransformationMessage : EntityBase
{
    [ForeignKey("ResponseTransformation")]
    public Guid ResponseTransformationId { get; set; }
    public string? Language { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Icon { get; set; }
}