using amorphie.core.Base;

public class DtoRoleGroup : DtoBase
{
    public ICollection<Translation> Titles { get; set; } = default!;    
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
}