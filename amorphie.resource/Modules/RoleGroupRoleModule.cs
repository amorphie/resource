using amorphie.resource;

public class RoleGroupRoleModule : BaseRoleGroupRoleModule<DtoRoleGroupRole, RoleGroupRole, RoleGroupRoleValidator>
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
