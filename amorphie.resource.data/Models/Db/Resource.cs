using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Resource : BaseDbEntityWithId
{
    public ICollection<Translation> DisplayNames { get; set; } = default!;
    public string? Type { get; set; }
    public string? Url { get; set; }
    public ICollection<Translation> Descriptions { get; set; } = default!;
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
}