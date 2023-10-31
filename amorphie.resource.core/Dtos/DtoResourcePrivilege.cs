using amorphie.core.Base;

public class DtoResourcePrivilege : DtoBase
{
    public Guid ResourceId { get; set; }
    public Guid PrivilegeId { get; set; }
    public int? Ttl { get; set; }
    public string? Status { get; set; }
}