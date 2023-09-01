using System.ComponentModel.DataAnnotations;
using amorphie.core.Base;
public class DtoPrivilege : DtoBase
{
    public Guid ResourceId { get; set; }
    public string? Url { get; set; }
    public int? Ttl { get; set; }
    public string? Status { get; set; }
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