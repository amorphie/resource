using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class ResourceClient : EntityBase
{
    [ForeignKey("Resource")]
    public Guid ResourceId { get; set; }
    public Guid ClientId { get; set; }
    public string? Status { get; set; }
}