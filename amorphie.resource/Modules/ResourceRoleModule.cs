using amorphie.resource;

public class ResourceRoleModule : BaseResourceRoleModule<DtoResourceRole, ResourceRole, ResourceRoleValidator>
{
    public ResourceRoleModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status" };

    public override string? UrlFragment => "resourceRole";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }
}
