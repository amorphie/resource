using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class ResourceGroupRole : EntityBase
{
    [ForeignKey("ResourceGroup")]
    public Guid ResourceGroupId { get; set; }

    [ForeignKey("Role")]
    public Guid RoleId { get; set; }

    public string? Status { get; set; }
}