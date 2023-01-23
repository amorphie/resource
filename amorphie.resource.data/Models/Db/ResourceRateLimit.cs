using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ResourceRateLimit
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("Resource")]
    public Guid ResourceId { get; set; }
    public Resource? Resource { get; set; }

    [ForeignKey("Role")]
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }

    public int Period { get; set; }
    public int Limit { get; set; }
    public int Enabled { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? CreatedUser { get; set; }
    public string? UpdatedUser { get; set; }
}