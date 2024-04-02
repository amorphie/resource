using amorphie.core.Extension;
using amorphie.core.Module.minimal_api;
using amorphie.core.Swagger;
using AutoMapper;
using Microsoft.OpenApi.Models;

public class ResourceModule : BaseBBTRoute<DtoResource, Resource, ResourceDBContext>
{
    public ResourceModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Type", "Url", "Status" };

    public override string? UrlFragment => "resource";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapPost("workflowStatus", saveResourceWithWorkflow);
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
        var resource = await context.Resources!.AsNoTracking()
         .Include(t => t.DisplayNames.Where(t => t.Language == httpContext.GetHeaderLanguage()))
         .Include(t => t.Descriptions.Where(t => t.Language == httpContext.GetHeaderLanguage()))
         .FirstOrDefaultAsync(t => t.Id == id, token);

        if (resource is Resource)
        {
            return TypedResults.Ok(ObjectMapper.Mapper.Map<DtoResource>(resource));
        }
        else
        {
            return Results.Problem(detail: "Resource Not Found", title: "Flow Exception", statusCode: 460);
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
        IQueryable<Resource> query = context
             .Set<Resource>()
             .AsNoTracking();

        if (!string.IsNullOrEmpty(sortColumn))
        {
            query = await query.Sort(sortColumn, sortDirection);
        }

        IList<Resource> resultList = await query
             .Include(t => t.DisplayNames.Where(t => t.Language == httpContext.GetHeaderLanguage()))
             .Include(t => t.Descriptions.Where(t => t.Language == httpContext.GetHeaderLanguage()))
             .Skip(page)
             .Take(pageSize)
             .ToListAsync(token);

        return (resultList != null && resultList.Count > 0)
                ? Results.Ok(mapper.Map<IList<DtoResource>>(resultList))
                : Results.NoContent();
    }
    public async ValueTask<IResult> saveResourceWithWorkflow(
        [FromBody] DtoPostWorkflow data,
        [FromServices] ResourceDBContext context,
        CancellationToken cancellationToken
        )
    {
        var existingRecord = await context!.Resources!.Include(i => i.DisplayNames)
        .Include(i => i.Descriptions).FirstOrDefaultAsync(t => t.Id == data.recordId);

        if (existingRecord == null)
        {
            var resource = ObjectMapper.Mapper.Map<Resource>(data.entityData!);
            resource.Id = data.recordId;
            context!.Resources!.Add(resource);
            await context.SaveChangesAsync(cancellationToken);
            return Results.Ok(resource);
        }
        else
        {
            //Apply update to only changed fields.
            if (SaveResourceUpdate(data.entityData!, existingRecord, context))
            {
                await context!.SaveChangesAsync(cancellationToken);
            }

            return Results.Ok();
        }
    }
    private static bool SaveResourceUpdate(DtoResource data, Resource existingRecord, ResourceDBContext context)
    {
        var hasChanges = false;
        // Apply update to only changed fields.
        if (data.Url != null && data.Url != existingRecord.Url)
        {
            existingRecord.Url = data.Url;
            hasChanges = true;
        }
        if (data.Type != null && data.Type != existingRecord.Type)
        {
            existingRecord.Type = data.Type;
            hasChanges = true;
        }
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
        if (data.DisplayNames != null && data.DisplayNames.Count > 0)
        {
            foreach (var name in data.DisplayNames)
            {
                amorphie.core.Base.Translation? translation = existingRecord.DisplayNames.FirstOrDefault(f => f.Language == name.Language);
                if (translation != null && translation.Label != name.Label)
                {
                    translation.Label = name.Label;
                    hasChanges = true;
                }
                if (translation == null)
                {
                    existingRecord.DisplayNames.Add(new amorphie.core.Base.Translation()
                    {
                        Label = name.Label,
                        Language = name.Language
                    });
                    hasChanges = true;
                }
            }
        }
        if (data.Descriptions != null && data.Descriptions.Count > 0)
        {
            foreach (var name in data.Descriptions)
            {
                amorphie.core.Base.Translation? translation = existingRecord.Descriptions.FirstOrDefault(f => f.Language == name.Language);
                if (translation != null && translation.Label != name.Label)
                {
                    translation.Label = name.Label;
                    hasChanges = true;
                }
                if (translation == null)
                {
                    existingRecord.Descriptions.Add(new amorphie.core.Base.Translation()
                    {
                        Label = name.Label,
                        Language = name.Language
                    });
                    hasChanges = true;
                }
            }
        }
        return hasChanges;
    }

}