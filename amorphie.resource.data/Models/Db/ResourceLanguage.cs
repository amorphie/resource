using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ResourceLanguage
{
    [Key]
    public Guid Id { get; set; }
    public string? TableName { get; set; }
    public Guid RowId { get; set; }
    public string? FieldName { get; set; }
    public string? Text { get; set; }
    public string? LanguageCode { get; set; }
    public int Order { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
    public Guid? CreatedByBehalfOf { get; set; }
    public Guid? ModifiedByBehalfOf { get; set; }
}