using System.Dynamic;
using System.Text.RegularExpressions;
using amorphie.resource.Modules.CheckAuthorize;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RulesEngine.Models;

public class CheckAuthorizeByRule : CheckAuthorizeBase, ICheckAuthorize
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
            .AsNoTracking().Where(x => (
                                           (x.ResourceId != null && x.ResourceId == resource.Id)
                                           ||
                                           (x.ResourceGroupId != null && x.ResourceGroupId == resource.ResourceGroupId)
                                       )
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
            bodyParamList = MapHelper.ToDictionary(jsonObject);
        }

        dynamic bodyObject = MapHelper.ToExpandoObject(bodyParamList);

        var ruleParamBody = new RuleParameter("body", bodyObject);

        ruleParams.Add(ruleParamBody);
    }
    
    private async ValueTask<List<RuleResultTree>> ExecuteRules(List<RuleParameter> ruleParameters,
        List<ResourceRule> resourceRules)
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
