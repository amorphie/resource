using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Resource
{
    [Key]
    public Guid Id { get; set; }
    public string? DisplayName { get; set; }
    public HttpMethodType? Type { get; set; }
    public string? Url { get; set; }
    public string? Description { get; set; }
    // public string[]? Tags { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
    public Guid? CreatedByBehalfOf { get; set; }
    public Guid? ModifiedByBehalfOf { get; set; }
}