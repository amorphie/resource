using System.Text.RegularExpressions;
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
         [FromBody] string url,
         [FromServices] ResourceDBContext context,
         HttpContext httpContext,
         CancellationToken cancellationToken
         )
    {
        Console.WriteLine("url: " + url);

        var resource = await context!.Resources!.AsNoTracking().FirstOrDefaultAsync(c => Regex.IsMatch(url, c.Url), cancellationToken);

        if (resource == null)
            return Results.Ok();

        var resourceClients = await context!.ResourceClients!
              .AsNoTracking().Where(x => x.ResourceId.Equals(resource.Id) && x.Status == "A")
              .ToListAsync(cancellationToken);

        if (resourceClients != null && resourceClients.Count != 0)
        {
            var hasClient = false;

            var headerClientId = httpContext.Request.Headers["clientId"];

            foreach (ResourceClient resourceClient in resourceClients)
            {
                if (resourceClient.ClientId.ToString() == headerClientId.ToString())
                {
                    hasClient = true;
                    break;
                }
            }

            if (!hasClient)
                return Results.Unauthorized();
        }

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
                Console.WriteLine($"{{header.{header.Key}}}" + " : " + header.Value);
            }

            foreach (var query in httpContext.Request.Query)
            {
                parameterList.Add($"{{query.{query.Key}}}", query.Value);
                Console.WriteLine($"{{query.{query.Key}}}" + " : " + query.Value);
            }

            Match match = Regex.Match(url, resource.Url);

            if (match.Success)
            {
                foreach (Group pathVariable in match.Groups)
                {
                    parameterList.Add($"{{path.var{pathVariable.Name}}}", pathVariable.Value);
                    Console.WriteLine($"{{path.var{pathVariable.Name}}}" + " : " + pathVariable.Value);
                }
            }

            var privilegeUrl = resourcePrivilege.Privilege.Url;

            Console.WriteLine("privilegeUrl:" + privilegeUrl);

            if (privilegeUrl != null)
            {
                foreach (var variable in parameterList)
                    privilegeUrl = privilegeUrl.Replace(variable.Key, variable.Value);

                Console.WriteLine("privilegeUrl2:" + privilegeUrl);

                var apiClient = new HttpClient();

                var response = await apiClient.GetAsync(privilegeUrl);

                Console.WriteLine("response:" + response.StatusCode);
                Console.WriteLine("IsSuccessStatusCode:" + response.IsSuccessStatusCode.ToString());

                if (!response.IsSuccessStatusCode)
                    return Results.Unauthorized();
            }
        }

        return Results.Ok();
    }
}