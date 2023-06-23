using amorphie.resource;

public class ScopeModule : BaseScopeModule<DtoScope, Scope, ScopeValidator>
{
    public ScopeModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status", "Reference", "Tags" };

    public override string? UrlFragment => "scope";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }
}
