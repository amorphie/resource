using amorphie.core.Module.minimal_api;

public class RoleGroupRoleModule: BaseBBTRoute<DtoRoleGroupRole, RoleGroupRole, ResourceDBContext>
{
    public RoleGroupRoleModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status" };

    public override string? UrlFragment => "roleGroupRole";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }
}
