using System.Text;
using System.Text.RegularExpressions;
using amorphie.core.Module.minimal_api;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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

        string checkAuthMethod = configuration["CheckAuthMethod"];

        if (checkAuthMethod == "Rule")
            return await CheckAuthorizeByRule(request, context, httpContext, headerClientId, configuration, cancellationToken);

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

        Console.WriteLine("parameterList:");

        foreach (KeyValuePair<string, object> kvp in parameterList)
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

        foreach (KeyValuePair<string, object> kvp in parameterList)
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
                    privilegeUrl = privilegeUrl.Replace(variable.Key, variable.Value.ToString());

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

    public async ValueTask<IResult> CheckAuthorizeByRule(
         CheckAuthorizeRequest request,
         ResourceDBContext context,
         HttpContext httpContext,
         string headerClientId,
         IConfiguration configuration,
         CancellationToken cancellationToken
         )
    {
        try
        {
            var resource = await GetResource(context, request, cancellationToken);

            if (resource == null)
                return Results.Ok();

            string allowEmptyPrivilege = configuration["AllowEmptyPrivilege"];

            var resourceRules = await GetResourceRules(context, resource, headerClientId, cancellationToken);

            if (resourceRules == null || resourceRules.Count == 0)
            {
                if (string.IsNullOrEmpty(allowEmptyPrivilege) || allowEmptyPrivilege == "True")
                    return Results.Ok();

                return Results.Unauthorized();
            }

            var ruleParams = new List<RuleParameter>();

            SetRuleParameterList(ruleParams, httpContext, request, resource.Url);

            var resultList = await ExecuteRules(ruleParams, resourceRules);

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

    private void SetRuleParameterList(
       List<RuleParameter> ruleParams,
       HttpContext httpContext,
       CheckAuthorizeRequest request,
       string? resourceUrl
       )
    {
        dynamic header = new ExpandoObject();

        foreach (var requestHeader in httpContext.Request.Headers)
        {
            ((IDictionary<string, object>)header).Add(requestHeader.Key, requestHeader.Value.ToString());
        }

        var ruleParamHeader = new RuleParameter("header", header);
        ruleParams.Add(ruleParamHeader);

        dynamic path = new ExpandoObject();
        var match = Regex.Match(request.Url, resourceUrl);
        if (match.Success)
        {
            foreach (Group pathVariable in match.Groups)
            {
                ((IDictionary<string, object>)path).Add($"var{pathVariable.Name}", pathVariable.Value);
            }
        }
        var ruleParamPath = new RuleParameter("path", path);
        ruleParams.Add(ruleParamPath);

        var bodyParamList = new Dictionary<string, object>();

        if (!string.IsNullOrEmpty(request.Data))
        {
            var jsonObject = JsonConvert.DeserializeObject<JObject>(request.Data);

            RecursiveJsonLoop(jsonObject, bodyParamList, "");
        }

        dynamic bodyObject = CreateExpandoObject(bodyParamList);

        var ruleParamBody = new RuleParameter("body", bodyObject);

        ruleParams.Add(ruleParamBody);
    }

    dynamic CreateExpandoObject(Dictionary<string, object> properties)
    {
        ExpandoObject expando = new ExpandoObject();
        IDictionary<string, object> dictionary = expando;

        foreach (var property in properties)
        {
            string propertyName = property.Key;
            object value = property.Value;

            string[] parts = propertyName.Split('.');
            CreateExpandoNestedObjects(dictionary, parts, value);
        }

        return expando;
    }

    void CreateExpandoNestedObjects(IDictionary<string, object> dictionary, string[] parts, object value)
    {
        IDictionary<string, object> currentDict = dictionary;

        for (int i = 0; i < parts.Length; i++)
        {
            string part = parts[i];
            bool isArray = part.Contains("[") && part.Contains("]");
            int index = -1;

            if (isArray)
            {
                int startIndex = part.IndexOf('[');
                int endIndex = part.IndexOf(']');
                index = int.Parse(part.Substring(startIndex + 1, endIndex - startIndex - 1));
                part = part.Substring(0, startIndex);
            }

            if (!currentDict.ContainsKey(part))
            {
                if (i == parts.Length - 1) // Last part, assign value
                {
                    if (isArray)
                    {
                        List<object> list = new List<object>();
                        currentDict.Add(part, list);
                        for (int j = 0; j <= index; j++)
                        {
                            if (j == index)
                            {
                                list.Add(value);
                            }
                            else
                            {
                                list.Add(null);
                            }
                        }
                    }
                    else
                    {
                        currentDict.Add(part, value);
                    }
                }
                else
                {
                    if (isArray)
                    {
                        List<object> list = new List<object>();
                        currentDict.Add(part, list);
                        IDictionary<string, object> nestedDict = new ExpandoObject();
                        list.Add(nestedDict);
                        currentDict = nestedDict;
                    }
                    else
                    {
                        IDictionary<string, object> nestedDict = new ExpandoObject();
                        currentDict.Add(part, nestedDict);
                        currentDict = nestedDict;
                    }
                }
            }
            else
            {
                if (isArray && i == parts.Length - 1) // Existing list, assign value
                {
                    var list = (List<object>)currentDict[part];
                    while (list.Count <= index)
                    {
                        list.Add(null);
                    }
                    list[index] = value;
                }
                currentDict = (IDictionary<string, object>)currentDict[part];
            }
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

    private async Task<List<ResourceRule>> GetResourceRules(
      ResourceDBContext context,
      Resource resource,
      string headerClientId,
      CancellationToken cancellationToken
      )
    {
        return await context!.ResourceRules!.Include(i => i.Rule)
                            .AsNoTracking().Where(x => (x.ResourceId == resource.Id || x.ResourceGroupId == resource.ResourceGroupId)
                                                       && (x.ClientId == null || x.ClientId.ToString() == headerClientId)
                                                       && x.Status == "A")
                            .OrderBy(x => x.Priority)
                            .ToListAsync(cancellationToken);
    }

    private async ValueTask<List<RuleResultTree>> ExecuteRules(List<RuleParameter> ruleParameters, List<ResourceRule> resourceRules)
    {
        var ruleDefinitions = new List<RuleDefinition>();

        foreach (ResourceRule resourceRule in resourceRules)
        {
            var ruleDefinition = new RuleDefinition
            {
                RuleName = resourceRule.Rule.Name,
                Expression = resourceRule.Rule.Expression
            };

            ruleDefinitions.Add(ruleDefinition);
        }

        var workflowRuleDefinition = new WorkflowRuleDefinition
        {
            WorkflowName = "workflow",
            Rules = ruleDefinitions.ToArray()
        };

        var workflowRules = new string[] { JsonConvert.SerializeObject(workflowRuleDefinition) };

        var reSettings = new ReSettings
        {
            CustomTypes = new Type[] { typeof(Utils) }
        };

        var rulesEngine = new RulesEngine.RulesEngine(workflowRules, reSettings);

        return await rulesEngine.ExecuteAllRulesAsync(workflowRuleDefinition.WorkflowName, ruleParameters.ToArray());
    }

    private void RecursiveJsonLoop(JObject jsonObject, Dictionary<string, object> keyValuePairs, string currentPath)
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
                keyValuePairs.Add($"{newPath}", property.Value.ToString());
            }
        }
    }
}