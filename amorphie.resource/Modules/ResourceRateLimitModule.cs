public static class ResourceRateLimitModule
{
    public static void MapResourceRateLimitEndpoints(this WebApplication app)
    {
        //getAllResourceRateLimits
        app.MapGet("/resourceRateLimit", getAllResourceRateLimits)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns all resource rate limits.";
                operation.Parameters[0].Description = "Paging parameter. **limit** is the page size of resultset.";
                operation.Parameters[1].Description = "Paging parameter. **Token** is returned from last query.";
                return operation;
            })
         .Produces<GetResourceRateLimitResponse[]>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);

        //saveResourceRateLimit
        app.MapPost("/resourceRateLimit", saveResourceRateLimit)
       .WithTopic("pubsub", "SaveResourceRateLimit")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested resource rate limit.";
                    return operation;
                })
                .Produces<GetResourceRateLimitResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);

        //deleteResourceRateLimit
        app.MapDelete("/resourceRateLimit/{resourceRateLimitId}", deleteResourceRateLimit)
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Deletes existing resource rate limit.";
                    return operation;
                })
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent);
    }

    static IResult saveResourceRateLimit(
        [FromBody] SaveResourceRateLimitRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        var existingRecord = context?.ResourceRateLimits?.FirstOrDefault(t => t.Id == data.id);

        if (existingRecord == null)
        {
            context!.ResourceRateLimits!.Add
            (
                new ResourceRateLimit
                {
                    Id = data.id,
                    ResourceId = data.resourceId,
                    Scope = data.scope,
                    Condition = data.condition,
                    Cron = data.cron,
                    Limit = data.limit,
                    Status = data.status,
                    CreatedAt = data.createdAt,
                    ModifiedAt = data.modifiedAt,
                    CreatedBy = data.createdBy,
                    ModifiedBy = data.modifiedBy,
                    CreatedByBehalfOf = data.createdByBehalfOf,
                    ModifiedByBehalfOf = data.modifiedByBehalfOf
                }
            );
            context.SaveChanges();
            return Results.Created($"/resourceRateLimit/{data.id}", data);
        }
        else
        {
            var hasChanges = false;

            ModuleHelper.PreUpdate(data.scope, existingRecord.Scope, ref hasChanges);
            ModuleHelper.PreUpdate(data.condition, existingRecord.Condition, ref hasChanges);
            ModuleHelper.PreUpdate(data.cron, existingRecord.Cron, ref hasChanges);
            ModuleHelper.PreUpdate(data.limit.ToString(), existingRecord.Limit.ToString(), ref hasChanges);
            ModuleHelper.PreUpdate(data.status, existingRecord.Status, ref hasChanges);

            if (hasChanges)
            {
                context!.SaveChanges();
                return Results.Ok(data);
            }
            else
            {
                return Results.Problem("Not Modified.", null, 304);
            }
        }
    }

    static IResult deleteResourceRateLimit(
     [FromRoute(Name = "resourceRateLimitId")] Guid resourceRateLimitId,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.ResourceRateLimits?.FirstOrDefault(t => t.Id == resourceRateLimitId);

        if (existingRecord == null)
        {
            return Results.NotFound();
        }
        else
        {
            context!.Remove(existingRecord);
            context.SaveChanges();
            return Results.NoContent();
        }
    }

    static IResult getAllResourceRateLimits(
        [FromServices] ResourceDBContext context,
        [FromQuery][Range(0, 100)] int page = 0,
        [FromQuery][Range(5, 100)] int pageSize = 100
        )
    {
        var query = context!.ResourceRateLimits!
            // .Include(t => t.Tags)
            .Skip(page * pageSize)
            .Take(pageSize);
       
        var resourceRateLimits = query.ToList();

        if (resourceRateLimits.Count() > 0)
        {
             return Results.Ok(resourceRateLimits.Select(res =>
              new GetResourceRateLimitResponse(
               res.Id,
               res.ResourceId,
               res.Scope,
               res.Condition,
               res.Cron,
               res.Limit,
               res.Status,
               res.CreatedAt,
               res.ModifiedAt,
               res.CreatedBy,
               res.ModifiedBy,
               res.CreatedByBehalfOf,
               res.ModifiedByBehalfOf
               )
            ).ToArray());
        }
        else
            return Results.NoContent();
    }
}