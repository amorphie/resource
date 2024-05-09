using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class ResourcePrivilege : EntityBase
{
    [ForeignKey("Resource")]
    public Guid? ResourceId { get; set; }

    [ForeignKey("Privilege")]
    public Guid PrivilegeId { get; set; }

    [ForeignKey("ResourceGroup")]
    public Guid? ResourceGroupId { get; set; }

    public Guid? ClientId { get; set; }

    public string? Status { get; set; }
    public int? Priority { get; set; }

    public Privilege Privilege { get; set; } = default!;
}