using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class RoleGroupRole : EntityBase
{
    [ForeignKey("RoleGroup")]
    public Guid RoleGroupId { get; set; }
    public RoleGroup? RoleGroup { get; set; }

    [ForeignKey("Role")]
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }

    public string? Status { get; set; }
}