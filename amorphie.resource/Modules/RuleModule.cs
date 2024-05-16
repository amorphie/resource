using amorphie.core.Module.minimal_api;

public class RuleModule : BaseBBTRoute<DtoRule, Rule, ResourceDBContext>
{
    public RuleModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Name", "Definition" };

    public override string? UrlFragment => "rule";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }
}