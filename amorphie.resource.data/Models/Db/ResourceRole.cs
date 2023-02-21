using System.ComponentModel.DataAnnotations.Schema;

public class ResourceRole : BaseDbEntityWithId
{
    [ForeignKey("Resource")]
    public Guid ResourceId { get; set; }
    public Resource? Resource { get; set; }

    [ForeignKey("Role")]
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }

    public string? Status { get; set; }   
}