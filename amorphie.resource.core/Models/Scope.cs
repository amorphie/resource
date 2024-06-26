using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;

public class Scope : EntityBase
{
    [ForeignKey("RoleGroup")]
    public Guid RoleGroupId { get; set; }
    public Guid? ClientId { get; set; }
    public ICollection<Translation> Titles { get; set; } = default!;
    public string? Reference { get; set; }
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
}