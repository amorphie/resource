using amorphie.core.Base;

public class DtoSavePrivilegeRequest : DtoBase
{
    public Guid ResourceId { get; set; }
    public int? Ttl { get; set; }
    public string? Status { get; set; }
}