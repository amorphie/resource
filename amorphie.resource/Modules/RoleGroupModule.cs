using amorphie.core.Module.minimal_api;
using amorphie.core.Swagger;
using amorphie.resource;
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
                CancellationToken token
                )
    {
        var resultList = await context!.RoleGroups!.AsNoTracking()
       .Include(t => t.Titles.Where(t => t.Language == httpContext.GetHeaderLanguage()))
       .Skip(page * pageSize)
       .Take(pageSize)
       .ToListAsync(token);

        if (resultList != null && resultList.Count() > 0)
        {
            var response = resultList.Select(x => ObjectMapper.Mapper.Map<DtoRoleGroup>(x)).ToList();

            return Results.Ok(response);
        }

        return Results.NoContent();
    }
}