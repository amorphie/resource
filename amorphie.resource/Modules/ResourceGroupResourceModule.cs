using amorphie.core.Module.minimal_api;

public class ResourceGroupResourceModule: BaseBBTRoute<DtoResourceGroupResource, ResourceGroupResource, ResourceDBContext>
{
    public ResourceGroupResourceModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status" };

    public override string? UrlFragment => "resourceGroupResource";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }
}
