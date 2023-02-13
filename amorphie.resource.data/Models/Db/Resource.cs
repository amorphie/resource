using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Resource : BaseDbEntityWithId
{
    [NotMapped]
    public string? DisplayName { get; set; }//ML

    public string? Type { get; set; }
    public string? Url { get; set; }

    [NotMapped]
    public string? Description { get; set; }//ML

    public string[]? Tags { get; set; }

    public string? Status { get; set; }  
}