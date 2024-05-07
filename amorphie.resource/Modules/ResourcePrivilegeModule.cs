using System.Text;
using System.Text.RegularExpressions;
using amorphie.core.Module.minimal_api;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using RulesEngine.Models;
using System.Dynamic;

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
        routeGroupBuilder.MapPost("checkAuthorize2", CheckAuthorize2);
    }

    public async ValueTask<IResult> CheckAuthorize2(
         [FromBody] CheckAuthorizeRequest request,
         [FromServices] ResourceDBContext context,
         HttpContext httpContext,
         [FromHeader(Name = "clientId")] string headerClientId,
         [FromServices] IConfiguration configuration,
         CancellationToken cancellationToken
         )
    {
        try
        {
            var resource = await GetResource(context, request, cancellationToken);

            if (resource == null)
                return Results.Ok();

            string allowEmptyPrivilege = configuration["AllowEmptyPrivilege"];

            var resourcePrivileges = await GetResourcePrivileges(context, resource, headerClientId, cancellationToken);

            if (resourcePrivileges == null || resourcePrivileges.Count == 0)
            {
                if (string.IsNullOrEmpty(allowEmptyPrivilege) || allowEmptyPrivilege == "True")
                    return Results.Ok();

                return Results.Unauthorized();
            }

            var parameterList = new Dictionary<string, string>();

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
                Console.WriteLine("requested Data :" + request.Data);
                JObject jsonObject = JsonConvert.DeserializeObject<JObject>(request.Data);

                RecursiveJsonLoop(jsonObject, parameterList, "body");
            }

            var ruleParameters = new List<RuleParameter>();

            var ruleParamHeaders = new RuleParameter("header", httpContext);
            ruleParameters.Add(ruleParamHeaders);

            var ruleParamPaths = new RuleParameter("path", new string[] { request.Url, resource.Url });
            ruleParameters.Add(ruleParamPaths);

            if (!string.IsNullOrEmpty(request.Data))
            {
                var ruleParamBody = new RuleParameter("body", request.Data);
                ruleParameters.Add(ruleParamBody);
            }

            var ruleParamApiParams = new RuleParameter("parameters", parameterList);
            ruleParameters.Add(ruleParamApiParams);

            var resultList = await ExecuteRules(ruleParameters);

            foreach (RuleResultTree ruleResult in resultList)
            {
                if (!ruleResult.IsSuccess)
                    return Results.Unauthorized();
            }

            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private async Task<Resource?> GetResource(
        ResourceDBContext context,
        CheckAuthorizeRequest request,
        CancellationToken cancellationToken
        )
    {
        return await context!.Resources!.AsNoTracking()
                                .FirstOrDefaultAsync(c => Regex.IsMatch(request.Url, c.Url), cancellationToken);
    }

    private async Task<List<ResourcePrivilege>> GetResourcePrivileges(
       ResourceDBContext context,
       Resource resource,
       string headerClientId,
       CancellationToken cancellationToken
       )
    {
        return await context!.ResourcePrivileges!.Include(i => i.Privilege)
                            .AsNoTracking().Where(x => (x.ResourceId == resource.Id || x.ResourceGroupId == resource.ResourceGroupId)
                                                       && (x.ClientId == null || x.ClientId.ToString() == headerClientId)
                                                       && x.Status == "A")
                            .OrderBy(x => x.Priority)
                            .ToListAsync(cancellationToken);
    }

    private async ValueTask<List<RuleResultTree>> ExecuteRules(List<RuleParameter> ruleParameters)
    {
        var ruleJson = "";

        using (StreamReader r = new StreamReader("wf1.json"))
        {
            ruleJson = r.ReadToEnd();
        }

        var workflowRules = new string[] { ruleJson };

        var reSettings = new ReSettings
        {
            CustomTypes = new Type[] { typeof(Utils) }
        };

        var workflowName = Helper.GetJsonValue(JObject.Parse(ruleJson), "WorkflowName");

        var rulesEngine = new RulesEngine.RulesEngine(workflowRules, reSettings);

        return await rulesEngine.ExecuteAllRulesAsync(workflowName, ruleParameters.ToArray());
    }
    public async ValueTask<IResult> CheckAuthorize(
         [FromBody] CheckAuthorizeRequest request,
         [FromServices] ResourceDBContext context,
         HttpContext httpContext,
         [FromHeader(Name = "clientId")] string headerClientId,
         [FromServices] IConfiguration configuration,
         CancellationToken cancellationToken
         )
    {
        Console.WriteLine("url: " + request.Url);

        var resource = await context!.Resources!.AsNoTracking()
                            .FirstOrDefaultAsync(c => Regex.IsMatch(request.Url, c.Url), cancellationToken);

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

        var parameterList = new Dictionary<string, string>();

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

        Console.WriteLine("parameterList:");

        foreach (KeyValuePair<string, string> kvp in parameterList)
        {
            Console.WriteLine(kvp.Key + ":" + kvp.Value);
        }

        if (!string.IsNullOrEmpty(request.Data))
        {
            Console.WriteLine("requested Data :" + request.Data);
            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(request.Data);

            RecursiveJsonLoop(jsonObject, parameterList, "body");
        }

        Console.WriteLine("parameterList2:");

        foreach (KeyValuePair<string, string> kvp in parameterList)
        {
            Console.WriteLine(kvp.Key + ":" + kvp.Value);
        }

        Console.WriteLine("request.Data: " + request.Data);
        Console.WriteLine("request.Url: " + request.Url);

        foreach (ResourcePrivilege resourcePrivilege in resourcePrivileges)
        {
            var privilegeUrl = resourcePrivilege.Privilege.Url;

            Console.WriteLine("privilegeUrl:" + privilegeUrl);

            if (privilegeUrl != null)
            {
                foreach (var variable in parameterList)
                    privilegeUrl = privilegeUrl.Replace(variable.Key, variable.Value);

                Console.WriteLine("privilegeUrl2:" + privilegeUrl);

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

                Console.WriteLine("response:" + response.StatusCode);
                Console.WriteLine("IsSuccessStatusCode:" + response.IsSuccessStatusCode.ToString());

                if (!response.IsSuccessStatusCode)
                    return Results.Unauthorized();
            }
        }

        return Results.Ok();
    }

    void RecursiveJsonLoop(JObject jsonObject, Dictionary<string, string> keyValuePairs, string currentPath)
    {
        foreach (var property in jsonObject.Properties())
        {
            string newPath = currentPath == "" ? property.Name : $"{currentPath}.{property.Name}";

            if (property.Value.Type == JTokenType.Object)
            {
                RecursiveJsonLoop((JObject)property.Value, keyValuePairs, newPath);
            }

            else if (property.Value.Type == JTokenType.Array)
            {
                for (int i = 0; i < ((JArray)property.Value).Count; i++)
                {
                    if (((JArray)property.Value)[i].Type == JTokenType.Object)
                        RecursiveJsonLoop((JObject)((JArray)property.Value)[i], keyValuePairs, $"{newPath}[{i}]");
                    else
                        keyValuePairs.Add($"{newPath}[{i}]", property.Value.ToString());
                }
            }
            else
            {
                keyValuePairs.Add($"{{{newPath}}}", property.Value.ToString());
            }
        }
    }

    
}