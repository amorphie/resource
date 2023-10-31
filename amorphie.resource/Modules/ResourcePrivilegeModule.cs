using System.Text.RegularExpressions;
using amorphie.core.Module.minimal_api;

public class ResourcePrivilegeModule: BaseBBTRoute<DtoResourcePrivilege, ResourcePrivilege, ResourceDBContext>
{
    public ResourcePrivilegeModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status", "Ttl" };

    public override string? UrlFragment => "resourcePrivilege";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        
        routeGroupBuilder.MapPost("checkAuthorize", CheckAuthorize);
    }

    public async ValueTask<IResult> CheckAuthorize(
         [FromBody] string url,
         [FromServices] ResourceDBContext context,
         HttpContext httpContext,
         CancellationToken cancellationToken
         )
    {
        var resource = await context!.Resources!.AsNoTracking().FirstOrDefaultAsync(c => Regex.IsMatch(url, c.Url), cancellationToken);

        if (resource == null)
            return Results.Ok();

        var resourcePrivilege = await context!.ResourcePrivileges!.Include(i => i.Privilege)
                        .AsNoTracking().FirstOrDefaultAsync(x => x.ResourceId.Equals(resource.Id) && x.Status == "A");

        if (resourcePrivilege == null)
            return Results.Ok();

        Dictionary<string, string> parameterList = new Dictionary<string, string>();

        foreach (var header in httpContext.Request.Headers)
            parameterList.Add($"{{header.{header.Key}}}", header.Value);

        foreach (var query in httpContext.Request.Query)
            parameterList.Add($"{{query.{query.Key}}}", query.Value);

        var privilegeUrl = resourcePrivilege.Privilege.Url;

        if (privilegeUrl != null)
        {
            foreach (var variable in parameterList)            
                privilegeUrl = privilegeUrl.Replace(variable.Key, variable.Value);            

            var apiClient = new HttpClient();

            var response = await apiClient.GetAsync(privilegeUrl);

            if (response.IsSuccessStatusCode)            
                return Results.Ok();            
        }

        return Results.Unauthorized();
    }
}
