using System.ComponentModel.DataAnnotations.Schema;

public class ResourceRateLimit : BaseDbEntityWithId
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