using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class ResourceGroupResource : EntityBase
{
    [ForeignKey("ResourceGroup")]
    public Guid ResourceGroupId { get; set; }

    [ForeignKey("Resource")]
    public Guid ResourceId { get; set; }

    public string? Status { get; set; }
}