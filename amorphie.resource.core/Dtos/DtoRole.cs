using amorphie.core.Base;

public class DtoRole : DtoBase
{
    public ICollection<MultilanguageText> Titles { get; set; } = default!;
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
}