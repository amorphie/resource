using amorphie.core.Base;

public class DtoResourceGroupResource : DtoBase
{
    public Guid ResourceGroupId { get; set; }

    public Guid ResourceId { get; set; }

    public string? Status { get; set; }
}