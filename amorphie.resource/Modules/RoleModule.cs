using amorphie.core.Extension;
using amorphie.core.Module.minimal_api;
using amorphie.core.Swagger;
using AutoMapper;
using Microsoft.OpenApi.Models;

public class RoleModule : BaseBBTRoute<DtoRole, Role, ResourceDBContext>
{
    public RoleModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status", "Tags" };

    public override string? UrlFragment => "role";

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
        var role = await context.Roles!.AsNoTracking()
         .Include(t => t.Titles.Where(t => t.Language == httpContext.GetHeaderLanguage()))
         .FirstOrDefaultAsync(t => t.Id == id, token);

        if (role is Role)
        {
            return TypedResults.Ok(ObjectMapper.Mapper.Map<DtoRole>(role));
        }
        else
        {
            return Results.Problem(detail: "Role Not Found", title: "Flow Exception", statusCode: 460);
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
                [FromQuery] SortDirectionEnum? sortDirection
                )
    {
        IQueryable<Role> query = context
              .Set<Role>()
              .AsNoTracking();

        if (!string.IsNullOrEmpty(sortColumn))
        {
            query = await query.Sort(sortColumn, sortDirection);
        }

        IList<Role> resultList = await query
            .Include(t => t.Titles.Where(t => t.Language == httpContext.GetHeaderLanguage()))
             .Skip(page * pageSize)
             .Take(pageSize)
             .ToListAsync(token);

        return (resultList != null && resultList.Count > 0)
                ? Results.Ok(mapper.Map<IList<DtoRole>>(resultList))
                : Results.NoContent();
    }

    public async ValueTask<IResult> saveWithWorkflow(
       [FromBody] DtoRoleWorkflow data,
       [FromServices] ResourceDBContext context,
       CancellationToken cancellationToken
       )
    {
        var existingRecord = await context!.Roles!.FirstOrDefaultAsync(t => t.Id == data.recordId);

        if (existingRecord == null)
        {
            var role = ObjectMapper.Mapper.Map<Role>(data.entityData!);
            role.Id = data.recordId;
            context!.Roles!.Add(role);
            await context.SaveChangesAsync(cancellationToken);
            return Results.Ok(role);
        }
        else
        {
            if (SaveRoleUpdate(data.entityData!, existingRecord, context))
            {
                await context!.SaveChangesAsync(cancellationToken);
            }

            return Results.Ok();
        }
    }

    private static bool SaveRoleUpdate(DtoRole data, Role existingRecord, ResourceDBContext context)
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
