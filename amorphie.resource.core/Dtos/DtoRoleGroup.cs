using amorphie.core.Base;

public class DtoRoleGroup : DtoBase
{
    public ICollection<MultilanguageText> Titles { get; set; } = default!;    
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
}