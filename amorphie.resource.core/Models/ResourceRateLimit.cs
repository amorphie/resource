using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class ResourceRateLimit : EntityBase
{
    [ForeignKey("Resource")]
    public Guid ResourceId { get; set; }
    public Resource? Resource { get; set; }

    public string? Scope { get; set; }
    public string? Condition { get; set; }
    public string? Cron { get; set; }
    public int Limit { get; set; }
    public string? Status { get; set; }  
}