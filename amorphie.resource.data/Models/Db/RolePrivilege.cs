using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RolePrivilege
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("Role")]
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }

    [ForeignKey("Privilege")]
    public Guid PrivilegeId { get; set; }
    public Privilege? Privilege { get; set; }

    public DateTime? CreatedDate { get; set; }
    public string? CreatedUser { get; set; }
}