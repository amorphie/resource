using Microsoft.AspNetCore.Mvc;
public static class ResourceRateLimitModule
{
    public static void MapResourceRateLimitEndpoints(this WebApplication app)
    {
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
        var existingRecord = context?.ResourceRateLimits?.FirstOrDefault(t => t.ResourceId == data.ResourceId && t.RoleId == data.RoleId);

        if (existingRecord == null)
        {
            var id = Guid.NewGuid();

            context!.ResourceRateLimits!.Add
            (
                new ResourceRateLimit
                {
                    Id = id,          
                    ResourceId = data.ResourceId,
                    RoleId = data.RoleId,  
                    Period = data.Period,
                    Limit = data.Limit,        
                    CreatedDate = data.createdDate,
                    UpdatedDate  = data.updatedDate,
                    CreatedUser = data.createdUser,
                    UpdatedUser = data.updatedUser
                }
            );
            context.SaveChanges();
            return Results.Created($"/resourceRateLimit/{id}", data);
        }
        else
        {
            // var hasChanges = false;

            // if (hasChanges)
            // {
                context!.SaveChanges();
                return Results.Ok(data);
            // }
            // else
            // {
            //     return Results.Problem("Not Modified.", null, 304);
            // }
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
}