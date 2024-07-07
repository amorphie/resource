using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using amorphie.resource.core.Enum;


public class RoleDefinition : EntityBase
{
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}