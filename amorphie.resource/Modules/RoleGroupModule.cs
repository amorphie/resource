using amorphie.resource;

public class RoleGroupModule : BaseRoleGroupModule<DtoRoleGroup, RoleGroup, RoleGroupValidator>
{
    public RoleGroupModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status", "Tags" };

    public override string? UrlFragment => "roleGroup";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }
}
