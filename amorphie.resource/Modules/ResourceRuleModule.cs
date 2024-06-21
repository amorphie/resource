using amorphie.core.Enums;
using amorphie.core.Identity;
using amorphie.core.Module.minimal_api;

public class ResourceRuleModule : BaseBBTRoute<DtoResourceRule, ResourceRule, ResourceDBContext>
{
    public ResourceRuleModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status" };

    public override string? UrlFragment => "resourceRule";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);

        routeGroupBuilder.MapPost("map", Map);
    }

    public async ValueTask<IResult> Map(
         [FromBody] DtoResourceRuleMapRequest request,
         [FromServices] ResourceDBContext context,
         [FromServices] IBBTIdentity bbtIdentity,
         CancellationToken cancellationToken
         )
    {
        var ruleIdList = await GetRuleIdList(request, context);
        var resourceIdList = await GetResourceIdList(request, context, bbtIdentity, cancellationToken);

        await AddResourceRules(resourceIdList, ruleIdList, context, bbtIdentity, cancellationToken);

        return Results.Ok();
    }

    private async Task<List<Guid>> GetRuleIdList(
                                    DtoResourceRuleMapRequest request,
                                    ResourceDBContext context
                                    )
    {
        var ruleNames = request.RuleName.Split(',');
        var ruleIdList = new List<Guid>();

        foreach (string ruleName in ruleNames)
        {
            var rule = await context!.Rules!.FirstOrDefaultAsync(t => t.Name == ruleName);

            if (rule != null)
            {
                ruleIdList.Add(rule.Id);
            }
        }

        return ruleIdList;
    }

    private async Task<List<Guid>> GetResourceIdList(
                                    DtoResourceRuleMapRequest request,
                                    ResourceDBContext context,
                                    [FromServices] IBBTIdentity bbtIdentity,
                                    CancellationToken cancellationToken
                                   )
    {
        var resourceIdList = new List<Guid>();

        var resource = await context!.Resources!.FirstOrDefaultAsync(t => t.Url == request.Url && t.Status == "A");

        if (resource == null)
        {
            var methodTypes = request.Method.Split(',');

            foreach (string method in methodTypes)
            {
                HttpMethodType httpMethodType = (HttpMethodType)Enum.Parse(typeof(HttpMethodType), method);

                var newResource = new Resource();
                newResource.CreatedAt = DateTime.UtcNow;
                newResource.CreatedBy = bbtIdentity.UserId.Value;
                newResource.CreatedByBehalfOf = bbtIdentity.BehalfOfId.Value;

                newResource.ModifiedAt = newResource.CreatedAt;
                newResource.ModifiedBy = newResource.CreatedBy;
                newResource.ModifiedByBehalfOf = newResource.CreatedByBehalfOf;

                newResource.Id = Guid.NewGuid();
                newResource.Status = "A";
                newResource.Type = httpMethodType;
                newResource.Url = request.Url;


                DbSet<Resource> dbSet = context.Set<Resource>();
                await dbSet.AddAsync(newResource);
                await context.SaveChangesAsync(cancellationToken);

                resourceIdList.Add(newResource.Id);
            }
        }
        else
        {
            resourceIdList.Add(resource.Id);
        }

        return resourceIdList;
    }

    private async Task AddResourceRules(
                                        List<Guid> resourceIdList,
                                        List<Guid> ruleIdList,
                                        ResourceDBContext context,
                                        [FromServices] IBBTIdentity bbtIdentity,
                                        CancellationToken cancellationToken
                                      )
    {
        foreach (Guid resourceId in resourceIdList)
        {
            for (int i = 0; i < ruleIdList.Count; i++)
            {
                var resourceRule = await context!.ResourceRules!
                    .FirstOrDefaultAsync(t => t.RuleId == ruleIdList[i] && t.ResourceId == resourceId && t.Status == "A");

                if (resourceRule == null)
                {
                    var newResourceRule = new ResourceRule();
                    newResourceRule.CreatedAt = DateTime.UtcNow;
                    newResourceRule.CreatedBy = bbtIdentity.UserId.Value;
                    newResourceRule.CreatedByBehalfOf = bbtIdentity.BehalfOfId.Value;

                    newResourceRule.ModifiedAt = newResourceRule.CreatedAt;
                    newResourceRule.ModifiedBy = newResourceRule.CreatedBy;
                    newResourceRule.ModifiedByBehalfOf = newResourceRule.CreatedByBehalfOf;

                    newResourceRule.Id = Guid.NewGuid();
                    newResourceRule.Status = "A";
                    newResourceRule.RuleId = ruleIdList[i];
                    newResourceRule.ResourceId = resourceId;
                    newResourceRule.Priority = i + 1;

                    DbSet<ResourceRule> dbSet = context.Set<ResourceRule>();
                    await dbSet.AddAsync(newResourceRule);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}