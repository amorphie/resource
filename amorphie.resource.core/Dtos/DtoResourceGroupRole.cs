using amorphie.core.Base;

public class DtoResourceGroupRole : DtoBase
{
    public Guid ResourceGroupId { get; set; }
    public Guid RoleId { get; set; }
    public string? Status { get; set; }
}