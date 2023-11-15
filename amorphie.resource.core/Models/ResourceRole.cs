using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class ResourceRole : EntityBase
{
    [ForeignKey("Resource")]
    public Guid ResourceId { get; set; }

    [ForeignKey("Role")]
    public Guid RoleId { get; set; }

    public string? Status { get; set; }
}