using System.ComponentModel.DataAnnotations;
using amorphie.core.Base;

public class DtoRole : DtoBase
{
    public ICollection<MultilanguageText> Titles { get; set; } = default!;
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
}

public class DtoRoleWorkflow
{
    public Guid recordId { get; set; }
    public DtoRole? entityData { get; set; }
    [Required]
    public string newStatus { get; set; } = default!;
    public Guid? user { get; set; }
    public Guid? behalfOfUser { get; set; }
    [Required]
    public string workflowName { get; set; } = default!;
}