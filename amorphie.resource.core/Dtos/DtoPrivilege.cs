using System.ComponentModel.DataAnnotations;
using amorphie.core.Base;
using amorphie.core.Enums;
public class DtoPrivilege : DtoBase
{
    public string? Url { get; set; }
    public HttpMethodType Type { get; set; }
}

public class DtoPrivilegeWorkflow
{
    public Guid recordId { get; set; }
    public DtoPrivilege? entityData { get; set; }
    [Required]
    public string newStatus { get; set; } = default!;
    public Guid? user { get; set; }
    public Guid? behalfOfUser { get; set; }
    [Required]
    public string workflowName { get; set; } = default!;
}