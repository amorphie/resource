using amorphie.core.Extension;
using amorphie.core.Module.minimal_api;
using amorphie.core.Swagger;
using AutoMapper;
using Microsoft.OpenApi.Models;

public class ScopeModule : BaseBBTRoute<DtoScope, Scope, ResourceDBContext>
{
    public ScopeModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Status", "Reference", "Tags" };

    public override string? UrlFragment => "scope";

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
        var scope = await context.Scopes!.AsNoTracking()
         .Include(t => t.Titles.Where(t => t.Language == httpContext.GetHeaderLanguage()))
         .FirstOrDefaultAsync(t => t.Id == id, token);

        if (scope is Scope)
        {
            return TypedResults.Ok(ObjectMapper.Mapper.Map<DtoScope>(scope));
        }
        else
        {
            return Results.Problem(detail: "Scope Not Found", title: "Flow Exception", statusCode: 460);
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
         IQueryable<Scope> query = context
             .Set<Scope>()
             .AsNoTracking();

        if (!string.IsNullOrEmpty(sortColumn))
        {
            query = await query.Sort(sortColumn, sortDirection);
        }

        IList<Scope> resultList = await query
            .Include(t => t.Titles.Where(t => t.Language == httpContext.GetHeaderLanguage()))
             .Skip(page)
             .Take(pageSize)
             .ToListAsync(token);

        return (resultList != null && resultList.Count > 0)
                ? Results.Ok(mapper.Map<IList<DtoScope>>(resultList))
                : Results.NoContent();
    }
}