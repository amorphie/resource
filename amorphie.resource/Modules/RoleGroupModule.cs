using amorphie.core.Extension;
using amorphie.core.Module.minimal_api;
using amorphie.core.Swagger;
using AutoMapper;
using Microsoft.OpenApi.Models;

public class RoleGroupModule : BaseBBTRoute<DtoRoleGroup, RoleGroup, ResourceDBContext>
{
    public RoleGroupModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status", "Tags" };

    public override string? UrlFragment => "roleGroup";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);

        routeGroupBuilder.MapPost("workflowStatus", saveWithWorkflow);
    }

    [AddSwaggerParameter("Language", ParameterLocation.Header, false)]
    protected override async ValueTask<IResult> GetMethod(
      [FromServices] ResourceDBContext context,
      [FromServices] IMapper mapper,
      [FromRoute(Name = "id")] Guid id,
      HttpContext httpContext,
      CancellationToken token
      )
    {
        var roleGroup = await context.RoleGroups!.AsNoTracking()
         .Include(t => t.Titles.Where(t => t.Language == httpContext.GetHeaderLanguage()))
         .FirstOrDefaultAsync(t => t.Id == id, token);

        if (roleGroup is RoleGroup)
        {
            return TypedResults.Ok(ObjectMapper.Mapper.Map<DtoRoleGroup>(roleGroup));
        }
        else
        {
            return Results.Problem(detail: "Role Group Not Found", title: "Flow Exception", statusCode: 460);
        }
    }

    [AddSwaggerParameter("Language", ParameterLocation.Header, false)]
    protected override async ValueTask<IResult> GetAllMethod(
                [FromServices] ResourceDBContext context,
                [FromServices] IMapper mapper,
                [FromQuery][Range(0, 100)] int page,
                [FromQuery][Range(5, 100)] int pageSize,
                HttpContext httpContext,
                CancellationToken token,
                [FromQuery] string? sortColumn,
                [FromQuery] SortDirectionEnum sortDirection = SortDirectionEnum.Asc
                )
    {
      IQueryable<RoleGroup> query = context
             .Set<RoleGroup>()
             .AsNoTracking();

        if (!string.IsNullOrEmpty(sortColumn))
        {
            query = await query.Sort(sortColumn, sortDirection);
        }

        IList<RoleGroup> resultList = await query
             .Include(t => t.Titles.Where(t => t.Language == httpContext.GetHeaderLanguage()))
             .Skip(page)
             .Take(pageSize)
             .ToListAsync(token);

        return (resultList != null && resultList.Count > 0)
                ? Results.Ok(mapper.Map<IList<DtoRoleGroup>>(resultList))
                : Results.NoContent();
    }

    public async ValueTask<IResult> saveWithWorkflow(
       [FromBody] DtoRoleGroupWorkflow data,
       [FromServices] ResourceDBContext context,
       CancellationToken cancellationToken
       )
    {
        var existingRecord = await context!.RoleGroups!.FirstOrDefaultAsync(t => t.Id == data.recordId);

        if (existingRecord == null)
        {
            var roleGroup = ObjectMapper.Mapper.Map<RoleGroup>(data.entityData!);
            roleGroup.Id = data.recordId;
            context!.RoleGroups!.Add(roleGroup);
            await context.SaveChangesAsync(cancellationToken);
            return Results.Ok(roleGroup);
        }
        else
        {
            if (SaveRoleGroupUpdate(data.entityData!, existingRecord, context))
            {
                await context!.SaveChangesAsync(cancellationToken);
            }

            return Results.Ok();
        }
    }

    private static bool SaveRoleGroupUpdate(DtoRoleGroup data, RoleGroup existingRecord, ResourceDBContext context)
    {
        var hasChanges = false;

        if (data.Status != null && data.Status != existingRecord.Status)
        {
            existingRecord.Status = data.Status;
            hasChanges = true;
        }
        if (data.Tags != null && data.Tags != existingRecord.Tags)
        {
            existingRecord.Tags = data.Tags;
            hasChanges = true;
        }

        return hasChanges;
    }
}