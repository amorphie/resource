public static class ResourceModule
{
    public static void MapResourceEndpoints(this WebApplication app)
    {
        //getAllResources
        app.MapGet("/resource", getAllResources)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns all resources.";
                operation.Parameters[0].Description = "Paging parameter. **limit** is the page size of resultset.";
                operation.Parameters[1].Description = "Paging parameter. **Token** is returned from last query.";
                operation.Parameters[2].Description = "RFC 5646 compliant language code.";
                return operation;
            })
         .Produces<GetResourceResponse[]>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);

        //getResource
        app.MapGet("/resource/{resourceId}", getResource)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns requested resource.";
                operation.Parameters[0].Description = "Id of the requested resource.";
                return operation;
            })
            .Produces<GetResourceResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        //saveResource
        app.MapPost("/resource", saveResource)
       .WithTopic("pubsub", "SaveResource")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested resource.";
                    return operation;
                })
                .Produces<GetResourceResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);

        //deleteResource
        app.MapDelete("/resource/{resourceId}", deleteResource)
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Deletes existing resource.";
                    return operation;
                })
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent);
    }

    static IResult saveResource(
        [FromBody] SaveResourceRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        var existingRecord = context?.Resources?.FirstOrDefault(t => t.Id == data.id);

        if (existingRecord == null)
        {
            context!.Resources!.Add
            (
                new Resource
                {
                    Id = data.id,
                    DisplayNames = data.displayNames,
                    Type = data.type,
                    Url = data.url,
                    Descriptions = data.descriptions,
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
            return Results.Created($"/resource/{data.id}", data);
        }
        else
        {
            var hasChanges = false;

            // Apply update to only changed fields.

            ModuleHelper.PreUpdate(data.url, existingRecord.Url, ref hasChanges);
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

    static IResult deleteResource(
     [FromRoute(Name = "resourceId")] Guid resourceId,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.Resources?.FirstOrDefault(t => t.Id == resourceId);

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

    static IResult getResource(
    [FromRoute(Name = "resourceId")] Guid resourceId,
    [FromServices] ResourceDBContext context
    )
    {
        var resource = context!.Resources!.Include(t => t.DisplayNames).Include(t => t.Descriptions)
            .FirstOrDefault(t => t.Id == resourceId);

        if (resource == null)
            return Results.NotFound();

        return Results.Ok(
           new GetResourceResponse(
             resource.Id,
             resource.DisplayNames.ToArray(),
             resource.Type,
             resource.Url,
             resource.Descriptions.ToArray(),
             resource.Tags,
             resource.Status,
             resource.CreatedAt,
             resource.ModifiedAt,
             resource.CreatedBy,
             resource.ModifiedBy,
             resource.CreatedByBehalfOf,
             resource.ModifiedByBehalfOf
            )
         );
    }

    static IResult getAllResources(
        [FromServices] ResourceDBContext context,
        [FromQuery][Range(0, 100)] int page = 0,
        [FromQuery][Range(5, 100)] int pageSize = 100,
        [FromHeader(Name = "Language")] string? language = "en-EN"
        )
    {
        var query = context!.Resources!
            .Include(t => t.DisplayNames.Where(t => t.Language == language))
            .Include(t => t.Descriptions.Where(t => t.Language == language))
            .Skip(page * pageSize)
            .Take(pageSize);

        var resources = query.ToList();

        if (resources.Count() > 0)
        {
            return Results.Ok(resources.Select(res =>
             new GetResourceResponse(
              res.Id,
              res.DisplayNames.ToArray(),
              res.Type,
              res.Url,
              res.Descriptions.ToArray(),
              res.Tags,
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

