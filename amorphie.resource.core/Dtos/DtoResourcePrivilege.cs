using amorphie.core.Base;

public class DtoResourcePrivilege : DtoBase
{
    public Guid ResourceId { get; set; }
    public Guid PrivilegeId { get; set; }
    public string? Status { get; set; }
    public int? Priority { get; set; }
}

public class CheckAuthorizeRequest
{
    public string? Url { get; set; }
    public string? Data { get; set; }
}