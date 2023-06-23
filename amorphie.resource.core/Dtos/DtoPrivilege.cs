using amorphie.core.Base;
public class DtoPrivilege : DtoBase
{
    public Guid ResourceId { get; set; }
    public string? Url { get; set; }
    public int? Ttl { get; set; }
    public string? Status { get; set; }
}