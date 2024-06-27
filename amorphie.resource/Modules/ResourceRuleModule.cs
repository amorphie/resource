using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Tokenizer;
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
        var resourceList = await GetResourceIdList(request, context, bbtIdentity, cancellationToken);

        await AddResourceRules(resourceList, ruleIdList, context, bbtIdentity, cancellationToken);

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

    private async Task<List<Resource>> GetResourceIdList(
                                    DtoResourceRuleMapRequest request,
                                    ResourceDBContext context,
                                    [FromServices] IBBTIdentity bbtIdentity,
                                    CancellationToken cancellationToken
                                   )
    {
        var existResourceList = new List<Resource>();

        var resources = await context!.Resources!.Where(t => t.Url == request.Url && t.Status == "A").ToListAsync(cancellationToken);
        var methodTypes = request.Method.Split(',');

        foreach (Resource resource in resources)
        {
            if (methodTypes.Contains(resource.Type.ToString()))
            {
                existResourceList.Add(resource);
            }
            else
            {
                resource.Status = "P";
                resource.ModifiedAt = DateTime.UtcNow; ;
                resource.ModifiedBy = bbtIdentity.UserId.Value;
                resource.ModifiedByBehalfOf = bbtIdentity.BehalfOfId.Value;
            }
        }

        foreach (string method in methodTypes)
        {
            var httpMethodType = (HttpMethodType)Enum.Parse(typeof(HttpMethodType), method);

            var resource = existResourceList!
                        .FirstOrDefault(t => t.Url == request.Url && t.Status == "A" && t.Type == httpMethodType);

            if (resource != null)
                continue;

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
            existResourceList.Add(newResource);
        }

        await context.SaveChangesAsync(cancellationToken);

        return existResourceList;
    }
    private async Task AddResourceRules(
                                        List<Resource> resourceList,
                                        List<Guid> ruleIdList,
                                        ResourceDBContext context,
                                        [FromServices] IBBTIdentity bbtIdentity,
                                        CancellationToken cancellationToken
                                      )
    {
        foreach (Resource resource in resourceList)
        {
            for (int i = 0; i < ruleIdList.Count; i++)
            {
                var resourceRule = await context!.ResourceRules!
                    .FirstOrDefaultAsync(t => t.RuleId == ruleIdList[i] && t.ResourceId == resource.Id && t.Status == "A");

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
                    newResourceRule.ResourceId = resource.Id;
                    newResourceRule.Priority = i + 1;

                    DbSet<ResourceRule> dbSet = context.Set<ResourceRule>();
                    await dbSet.AddAsync(newResourceRule);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }

            var resourceRules = await context!.ResourceRules!
                    .Where(t => t.ResourceId == resource.Id && t.Status == "A").ToListAsync(cancellationToken);

            foreach (ResourceRule resourceRule1 in resourceRules)
            {
                if (ruleIdList.Contains(resourceRule1.RuleId) == false)
                {
                    resourceRule1.Status = "P";
                    resourceRule1.ModifiedAt = DateTime.UtcNow; ;
                    resourceRule1.ModifiedBy = bbtIdentity.UserId.Value;
                    resourceRule1.ModifiedByBehalfOf = bbtIdentity.BehalfOfId.Value;
                }
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}