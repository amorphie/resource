using System.Text;
using System.Text.RegularExpressions;
using amorphie.resource.Helper;
using amorphie.resource.Modules.CheckAuthorize;
using Elastic.Apm.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ILogger = Microsoft.Extensions.Logging.ILogger;

public class CheckAuthorizeByPrivilege : CheckAuthorizeBase, ICheckAuthorize
{
    public async ValueTask<IResult> Check(
        CheckAuthorizeRequest request,
        ResourceDBContext context,
        HttpContext httpContext,
        string headerClientId,
        IConfiguration configuration,
        ILogger logger,
        CancellationToken cancellationToken
    )
    {
        var resource = await GetResource(context, request, cancellationToken);

        if (resource == null)
        {
            return Results.Ok(new CheckAuthorizeOutput("Resource not found."));
        }

        string allowEmptyPrivilege = configuration["AllowEmptyPrivilege"];

        var resourcePrivileges = await context!.ResourcePrivileges!.Include(i => i.Privilege)
            .AsNoTracking().Where(x => (
                                           (x.ResourceId != null && x.ResourceId == resource.Id)
                                           ||
                                           (x.ResourceGroupId != null && x.ResourceGroupId == resource.ResourceGroupId)
                                       )
                                       && (x.ClientId == null || x.ClientId.ToString() == headerClientId)
                                       && x.Status == "A")
            .OrderBy(x => x.Priority)
            .ToListAsync(cancellationToken);

        if (resourcePrivileges == null || resourcePrivileges.Count == 0)
        {
            if (string.IsNullOrEmpty(allowEmptyPrivilege) || allowEmptyPrivilege == "True")
            {
                return Results.Ok(new CheckAuthorizeOutput("Allow empty privilege active."));
            }

            return Results.Json(new CheckAuthorizeOutput("Resource rules not found."), statusCode: 403);
        }

        var parameterList = new Dictionary<string, object>();

        foreach (var header in httpContext.Request.Headers)
        {
            parameterList.Add($"{{header.{header.Key.ToClean()}}}", header.Value);
        }

        var match = Regex.Match(request.Url, resource.Url);

        if (match.Success)
        {
            foreach (Group pathVariable in match.Groups)
            {
                parameterList.Add($"{{path.var{pathVariable.Name}}}", pathVariable.Value);
            }
        }

        if (!string.IsNullOrEmpty(request.Data))
        {
            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(request.Data);

            RecursiveJsonLoop(jsonObject, parameterList, "body");
        }

        var transaction = Elastic.Apm.Agent.Tracer.CurrentTransaction ??
                          Elastic.Apm.Agent.Tracer.StartTransaction("CallApi", ApiConstants.TypeUnknown);

        foreach (ResourcePrivilege resourcePrivilege in resourcePrivileges)
        {
            var privilegeUrl = resourcePrivilege.Privilege.Url;

            if (privilegeUrl != null)
            {
                foreach (var variable in parameterList)
                    privilegeUrl = privilegeUrl.Replace(variable.Key, variable.Value.ToString());
                
                var apiClientService = new HttpClientService();
                var span = transaction.StartSpan($"CallApi-{privilegeUrl}", ApiConstants.TypeUnknown);
                try
                {
                    var headers = new Dictionary<string, object>();
                    foreach (var header in httpContext.Request.Headers)
                    {
                        headers.Add(header.Key, header.Value.ToString());
                    }
                
                    var response = await apiClientService.SendRequestAsync(
                        privilegeUrl,
                        string.IsNullOrEmpty(request.Data) ? "" : request.Data,
                        resourcePrivilege.Privilege.Type.ToString(),
                        headers,
                        span);
                    
                    if (!response.IsSuccessStatusCode)
                    {
                        return Results.Json(new CheckAuthorizeOutput("FAILED"), statusCode: 403);
                    }
                }
                catch (Exception e)
                {
                    span.CaptureException(e);
                }
                finally
                {
                    span.End();
                }
            }
        }

        return Results.Ok(new CheckAuthorizeOutput("SUCCESS"));
    }
}
