using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ResourceRole
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("Resource")]
    public Guid ResourceId { get; set; }
    public Resource? Resource { get; set; }

    [ForeignKey("Role")]
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }

    public DateTime? CreatedDate { get; set; }
    public string? CreatedUser { get; set; }
}