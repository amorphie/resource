using amorphie.core.Base;

public class DtoResourceRateLimit : DtoBase
{

    public Guid ResourceId { get; set; }
    public string? Scope { get; set; }
    public string? Condition { get; set; }
    public string? Cron { get; set; }
    public int? Limit { get; set; }
    public string? Status { get; set; }
}