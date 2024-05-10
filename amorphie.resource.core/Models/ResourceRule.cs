using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class ResourceRule : EntityBase
{
    [ForeignKey("Resource")]
    public Guid? ResourceId { get; set; }

    [ForeignKey("ResourceGroup")]
    public Guid? ResourceGroupId { get; set; }
    
    [ForeignKey("Rule")]
    public Guid RuleId { get; set; }

    public Guid? ClientId { get; set; }

    public string? Status { get; set; }

    public int Priority { get; set; }

    public Rule Rule { get; set; } = default!;
}