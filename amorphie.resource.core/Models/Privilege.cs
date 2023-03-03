using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class Privilege : EntityBase
{
    [ForeignKey("Resource")]
    public Guid ResourceId { get; set; }   
    public int? Ttl { get; set; }
    public string? Status { get; set; } 
}