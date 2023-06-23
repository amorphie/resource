using amorphie.resource;

public class PrivilegeModule : BasePrivilegeModule<DtoPrivilege, Privilege, PrivilegeValidator>
{
    public PrivilegeModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Ttl", "Url", "Status" };

    public override string? UrlFragment => "privilege";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }
}
