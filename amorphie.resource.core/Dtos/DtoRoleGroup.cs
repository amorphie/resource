using System.ComponentModel.DataAnnotations;
using amorphie.core.Base;

public class DtoRoleGroup : DtoBase
{
    public ICollection<MultilanguageText> Titles { get; set; } = default!;    
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
}

public class DtoRoleGroupWorkflow
{
    public Guid recordId { get; set; }
    public DtoRoleGroup? entityData { get; set; }
    [Required]
    public string newStatus { get; set; } = default!;
    public Guid? user { get; set; }
    public Guid? behalfOfUser { get; set; }
    [Required]
    public string workflowName { get; set; } = default!;
}