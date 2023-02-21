using System.ComponentModel.DataAnnotations.Schema;

public class Privilege : BaseDbEntityWithId
{
    [ForeignKey("Resource")]
    public Guid ResourceId { get; set; }    
    public Resource? Resource { get; set; }
    
    public string? Url { get; set; }
    public int Ttl { get; set; }
    public string? Status { get; set; } 
}