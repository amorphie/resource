using amorphie.core.Module.minimal_api;
using amorphie.core.Swagger;
using amorphie.resource;
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
                CancellationToken token
                )
    {
        var resultList = await context!.Roles!.AsNoTracking()
       .Include(t => t.Titles.Where(t => t.Language == httpContext.GetHeaderLanguage()))
       .Skip(page * pageSize)
       .Take(pageSize)
       .ToListAsync(token);

        if (resultList != null && resultList.Count() > 0)
        {
            var response = resultList.Select(x => ObjectMapper.Mapper.Map<DtoRole>(x)).ToList();

            return Results.Ok(response);
        }

        return Results.NoContent();
    }
}