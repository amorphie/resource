using amorphie.core.Base;

public class DtoScope : DtoBase
{
    public Guid RoleGroupId { get; set; }
    public ICollection<MultilanguageText> Titles { get; set; } = default!;
    public string? Reference { get; set; }
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
}