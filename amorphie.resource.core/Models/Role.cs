using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using amorphie.resource.core.Enum;


public class Role : EntityBase
{
    public ICollection<Translation> Titles { get; set; } = default!;
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
    [ForeignKey("Definition")]
    public Guid DefinitionId{get;set;}
    public RoleDefinition Definition { get; set; } = default!;
}