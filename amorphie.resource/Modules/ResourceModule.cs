using amorphie.resource;

public class ResourceModule : BaseResourceModule<DtoResource, Resource, ResourceValidator>
{
    public ResourceModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Type", "Url", "Status" };

    public override string? UrlFragment => "resource";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }
}
