using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Resource
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? DisplayName { get; set; }
    public string? Url { get; set; }
    public string? Description { get; set; }
    public int Enabled { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? CreatedUser { get; set; }
    public string? UpdatedUser { get; set; }

}