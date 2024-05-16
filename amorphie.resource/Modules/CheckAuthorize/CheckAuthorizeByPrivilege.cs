using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class CheckAuthorizeByPrivilege : CheckAuthorizeBase, ICheckAuthorize
{
    public async ValueTask<IResult> Check(
                                    CheckAuthorizeRequest request,
                                    ResourceDBContext context,
                                    HttpContext httpContext,
                                    string headerClientId,
                                    IConfiguration configuration,
                                    CancellationToken cancellationToken
                             )
    {
        var resource = await GetResource(context, request, cancellationToken);

        if (resource == null)
            return Results.Ok();

        string allowEmptyPrivilege = configuration["AllowEmptyPrivilege"];

        var resourcePrivileges = await context!.ResourcePrivileges!.Include(i => i.Privilege)
                        .AsNoTracking().Where(x => (x.ResourceId == resource.Id || x.ResourceGroupId == resource.ResourceGroupId)
                                                   && (x.ClientId == null || x.ClientId.ToString() == headerClientId)
                                                   && x.Status == "A")
                        .OrderBy(x => x.Priority)
                        .ToListAsync(cancellationToken);

        if (resourcePrivileges == null || resourcePrivileges.Count == 0)
        {
            if (string.IsNullOrEmpty(allowEmptyPrivilege) || allowEmptyPrivilege == "True")
                return Results.Ok();

            return Results.Unauthorized();
        }

        var parameterList = new Dictionary<string, object>();

        foreach (var header in httpContext.Request.Headers)
        {
            parameterList.Add($"{{header.{header.Key}}}", header.Value);
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

        foreach (ResourcePrivilege resourcePrivilege in resourcePrivileges)
        {
            var privilegeUrl = resourcePrivilege.Privilege.Url;

            if (privilegeUrl != null)
            {
                foreach (var variable in parameterList)
                    privilegeUrl = privilegeUrl.Replace(variable.Key, variable.Value.ToString());

                var apiClient = new HttpClient();

                HttpResponseMessage response;

                if (resourcePrivilege.Privilege.Type == amorphie.core.Enums.HttpMethodType.POST)
                {
                    var data = string.IsNullOrEmpty(request.Data) ? "" : request.Data;
                    var httpContent = new StringContent(data, Encoding.UTF8, "application/json");

                    response = await apiClient.PostAsync(privilegeUrl, httpContent);
                }
                else
                {
                    response = await apiClient.GetAsync(privilegeUrl);
                }

                if (!response.IsSuccessStatusCode)
                    return Results.Unauthorized();
            }
        }

        return Results.Ok();
    }
}