using amorphie.core.Module.minimal_api;

public class ResourceRuleModule : BaseBBTRoute<DtoResourceRule, ResourceRule, ResourceDBContext>
{
    public ResourceRuleModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status" };

    public override string? UrlFragment => "resourceRule";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }
}