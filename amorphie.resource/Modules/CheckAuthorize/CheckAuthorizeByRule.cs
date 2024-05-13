using System.Dynamic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RulesEngine.Models;

public class CheckAuthorizeByRule : CheckAuthorizeBase
{
    public override async ValueTask<IResult> Check(
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

            if (resultList.Any(t => t.IsSuccess == false))
                return Results.Unauthorized();

            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
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
}