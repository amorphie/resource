using amorphie.core.Module.minimal_api;

public class ResourceGroupPrivilegeModule : BaseBBTRoute<DtoResourceGroupPrivilege, ResourceGroupPrivilege, ResourceDBContext>
{
    public ResourceGroupPrivilegeModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status" };

    public override string? UrlFragment => "resourceGroupPrivilege";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }
}