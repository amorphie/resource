using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Privilege
{
    [Key]
    public Guid Id { get; set; } 

    [ForeignKey("Resource")]
    public Guid ResourceId { get; set; }
    public Resource? Resource { get; set; }

    public string? Url { get; set; }
    public int Ttl { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
    public Guid? CreatedByBehalfOf { get; set; }
    public Guid? ModifiedByBehalfOf { get; set; }
}