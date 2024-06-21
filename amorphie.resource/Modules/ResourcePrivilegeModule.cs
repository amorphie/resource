using amorphie.core.Module.minimal_api;

public class ResourcePrivilegeModule : BaseBBTRoute<DtoResourcePrivilege, ResourcePrivilege, ResourceDBContext>
{
    public ResourcePrivilegeModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status" };

    public override string? UrlFragment => "resourcePrivilege";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);

        routeGroupBuilder.MapPost("checkAuthorize", CheckAuthorize);
    }

    public async ValueTask<IResult> CheckAuthorize(
         [FromBody] CheckAuthorizeRequest request,
         [FromServices] ResourceDBContext context,
         HttpContext httpContext,
         [FromHeader(Name = "clientId")] string headerClientId,
         [FromServices] IConfiguration configuration,
         [FromQuery] string? checkAuthMethod,
         CancellationToken cancellationToken
         )
    {
        ICheckAuthorize checkAuthorize;

        if (checkAuthMethod == "Rule")
        {
            checkAuthorize = new CheckAuthorizeByRule();
        }
        else
        {
            checkAuthorize = new CheckAuthorizeByPrivilege();
        }

        return await checkAuthorize.Check(request, context, httpContext, headerClientId, configuration, cancellationToken);
    }
}