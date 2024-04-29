using amorphie.core.Base;

public class DtoResourceGroupPrivilege : DtoBase
{
    public Guid ResourceGroupId { get; set; }
    public Guid PrivilegeId { get; set; }
    public Guid ClientId { get; set; }
    public string? Status { get; set; }
    public int? Priority { get; set; }
}