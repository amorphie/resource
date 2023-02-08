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
    
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
    public Guid? CreatedByBehalfOf { get; set; }
    public Guid? ModifiedByBehalfOf { get; set; }
}