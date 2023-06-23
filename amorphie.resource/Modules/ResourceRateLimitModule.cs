using amorphie.resource;

public class ResourceRateLimitModule : BaseResourceRateLimitModule<DtoResourceRateLimit, ResourceRateLimit, ResourceRateLimitValidator>
{
    public ResourceRateLimitModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Scope", "Condition", "Cron", "Limit", "Status" };

    public override string? UrlFragment => "resourceRateLimit";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }
}
