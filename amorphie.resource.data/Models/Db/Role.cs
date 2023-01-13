using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Role
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }    
    public int Enabled { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? CreatedUser { get; set; }
    public string? UpdatedUser { get; set; }
}