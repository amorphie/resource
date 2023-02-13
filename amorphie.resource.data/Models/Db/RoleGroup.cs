using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RoleGroup : BaseDbEntityWithId
{
    [NotMapped]
    public string? Title { get; set; } //ML   
    
    public string[]? Tags { get; set; }
    public string? Status { get; set; }

}