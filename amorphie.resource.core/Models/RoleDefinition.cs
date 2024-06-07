using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using amorphie.resource.core.Enum;


public class RoleDefinition : EntityBase
{
    public ICollection<Translation> Titles { get; set; } = default!;
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
    public Guid ClientId { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}