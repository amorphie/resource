using amorphie.core.Module.minimal_api;
public class ResourceRateLimitModule : BaseBBTRoute<DtoResourceRateLimit, ResourceRateLimit, ResourceDBContext>
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
