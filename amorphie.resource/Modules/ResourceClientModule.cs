using amorphie.core.Module.minimal_api;

public class ResourceClientModule : BaseBBTRoute<DtoResourceClient, ResourceClient, ResourceDBContext>
{
    public ResourceClientModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status" };

    public override string? UrlFragment => "resourceClient";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }
}
