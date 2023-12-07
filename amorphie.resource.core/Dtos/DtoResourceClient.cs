using amorphie.core.Base;

public class DtoResourceClient : DtoBase
{
    public Guid ResourceId { get; set; }
    public Guid ClientId { get; set; }
    public string? Status { get; set; }
}