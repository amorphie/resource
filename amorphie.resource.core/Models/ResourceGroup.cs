using amorphie.core.Base;

public class ResourceGroup : EntityBase
{
    public ICollection<Translation> Titles { get; set; } = default!;    
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
}