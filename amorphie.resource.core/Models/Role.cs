using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;


public class Role : EntityBase
{
    public ICollection<Translation> Titles { get; set; } = default!;
    public string[]? Tags { get; set; }
    public string? Status { get; set; }
}