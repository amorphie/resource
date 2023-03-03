using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class RoleGroupRole : EntityBase
{
    [ForeignKey("RoleGroup")]
    public Guid RoleGroupId { get; set; }

    [ForeignKey("Role")]
    public Guid RoleId { get; set; }

    public string? Status { get; set; }
}