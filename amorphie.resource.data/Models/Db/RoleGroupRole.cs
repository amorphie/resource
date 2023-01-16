using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RoleGroupRole
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("RoleGroup")]
    public Guid RoleGroupId { get; set; }
    public RoleGroup? RoleGroup { get; set; }

    [ForeignKey("Role")]
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }

    public DateTime? CreatedDate { get; set; }
    public string? CreatedUser { get; set; }
}