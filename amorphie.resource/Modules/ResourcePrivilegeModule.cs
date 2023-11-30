using System.Text.RegularExpressions;
using amorphie.core.Module.minimal_api;
using System.Text.RegularExpressions;

public class ResourcePrivilegeModule : BaseBBTRoute<DtoResourcePrivilege, ResourcePrivilege, ResourceDBContext>
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

        var resourcePrivileges = await context!.ResourcePrivileges!.Include(i => i.Privilege)
                        .AsNoTracking().Where(x => x.ResourceId.Equals(resource.Id) && x.Status == "A")
                        .OrderBy(x => x.Priority)
                        .ToListAsync(cancellationToken);

        if (resourcePrivileges == null || resourcePrivileges.Count == 0)
            return Results.Ok();

        foreach (ResourcePrivilege resourcePrivilege in resourcePrivileges)
        {
            Dictionary<string, string> parameterList = new Dictionary<string, string>();

            foreach (var header in httpContext.Request.Headers)
            {
                parameterList.Add($"{{header.{header.Key}}}", header.Value);
                Console.WriteLine($"{{header.{header.Key}}}" + header.Value);
            }


            foreach (var query in httpContext.Request.Query)
                parameterList.Add($"{{query.{query.Key}}}", query.Value);

            Match match = Regex.Match(url, resource.Url);
            if (match.Success)
            {
                foreach (Group pathVariable in match.Groups)
                {
                    parameterList.Add($"{{path.var{pathVariable.Name}}}", pathVariable.Value);
                }
            }

            var privilegeUrl = resourcePrivilege.Privilege.Url;

            if (privilegeUrl != null)
            {
                foreach (var variable in parameterList)
                    privilegeUrl = privilegeUrl.Replace(variable.Key, variable.Value);

                var apiClient = new HttpClient();

                Console.WriteLine("privilegeUrl:" + privilegeUrl);

                var response = await apiClient.GetAsync(privilegeUrl);

                if (!response.IsSuccessStatusCode)
                    return Results.Unauthorized();
            }
        }

        return Results.Ok();
    }
}