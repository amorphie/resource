using amorphie.core.Module.minimal_api;
using Newtonsoft.Json;

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
        ILogger<ResourcePrivilegeModule> logger,
        CancellationToken cancellationToken
    )
    {
        var transaction = Elastic.Apm.Agent.Tracer.CurrentTransaction;
        transaction.SetLabel("ClientId", headerClientId);
        transaction.SetLabel("RequestBody.Url", request.Url);
        transaction.SetLabel("RequestBody.Method", request.Method);
        transaction.SetLabel("RequestBody.Data", request.Data);

        ICheckAuthorize checkAuthorize;

        if (checkAuthMethod == "Rule")
        {
            logger.LogInformation(
                $"Request.CheckAuthMethod:Rule | ClientId:{headerClientId} | Request.Url:{request.Url} | Request.Data:{request.Data} | Request.Method:{request.Method}");
            checkAuthorize = new CheckAuthorizeByRule();
        }
        else
        {
            logger.LogInformation(
                $"Request.CheckAuthMethod:None | ClientId:{headerClientId} | Request.Url:{request.Url} | Request.Data:{request.Data} | Request.Method:{request.Method}");
            checkAuthorize = new CheckAuthorizeByPrivilege();
        }

        return await checkAuthorize.Check(request, context, httpContext, headerClientId, configuration, logger,
            cancellationToken);
    }
}
