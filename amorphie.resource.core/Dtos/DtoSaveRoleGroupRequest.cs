using amorphie.core.Base;

public class DtoSaveRoleGroupRequest
{
    public Guid? Id { get; set; }
    public ICollection<MultilanguageText> Titles { get; set; } = default!;
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? CreatedByBehalfOf { get; set; }
    public Guid ModifiedBy { get; set; }
    public Guid? ModifiedByBehalfOf { get; set; }
}