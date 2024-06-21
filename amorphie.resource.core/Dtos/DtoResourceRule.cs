using amorphie.core.Base;

public class DtoResourceRule : DtoBase
{
    public Guid? ResourceId { get; set; }
    public Guid? ResourceGroupId { get; set; }
    public Guid RuleId { get; set; }
    public Guid? ClientId { get; set; }
    public string? Status { get; set; }
    public int Priority { get; set; }
}

public class DtoResourceRuleMapRequest
{
    public string Url { get; set; }
    public string Method { get; set; }
    public string RuleName { get; set; }
}