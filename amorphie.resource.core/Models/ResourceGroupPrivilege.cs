using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class ResourceGroupPrivilege : EntityBase
{
    [ForeignKey("ResourceGroup")]
    public Guid ResourceGroupId { get; set; }

    [ForeignKey("Privilege")]
    public Guid PrivilegeId { get; set; }

    public Guid? ClientId { get; set; }
    public string? Status { get; set; }
    public int? Priority { get; set; }

    public Privilege Privilege { get; set; } = default!;
}