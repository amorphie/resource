using amorphie.core.Module.minimal_api;
using amorphie.resource;

public class ResourceGroupRoleModule : BaseBBTRoute<DtoResourceGroupRole, ResourceGroupRole, ResourceDBContext>
{
    public ResourceGroupRoleModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status" };

    public override string? UrlFragment => "resourceGroupRole";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }
}