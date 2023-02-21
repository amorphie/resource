using System.ComponentModel.DataAnnotations.Schema;

public class RoleGroupRole : BaseDbEntityWithId
{
    [ForeignKey("RoleGroup")]
    public Guid RoleGroupId { get; set; }
    public RoleGroup? RoleGroup { get; set; }

    [ForeignKey("Role")]
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }

    public string? Status { get; set; }
}