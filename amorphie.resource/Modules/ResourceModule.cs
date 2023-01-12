using Microsoft.AspNetCore.Mvc;

public static class ResourceModule
{
    public static void MapResourceEndpoints(this WebApplication app)
    {
        //searchResource
        app.MapGet("/resource", searchResource)
            .WithOpenApi(operation =>
                {
                    operation.Summary = "Returns queried resources.";
                    operation.Parameters[0].Description = "Full or partial name of resource name to be queried.";
                    return operation;
                })
            .Produces<GetResourceResponse[]>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);

        //getResource
        app.MapGet("/resource/{resourceName}", getResource)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns requested resource.";
                operation.Parameters[0].Description = "Name of the requested resource.";
                return operation;
            })
            .Produces<GetResourceResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        //saveResource
        app.MapPost("/resource", saveResource)
       .WithTopic("pubsub", "SaveResource")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested tag.";
                    return operation;
                })
                .Produces<GetResourceResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);

        //deleteResource
        app.MapDelete("/resource/{resourceName}", deleteResource)
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
        var existingRecord = context?.Resources?.FirstOrDefault(t => t.Name == data.name);

        if (existingRecord == null)
        {
            context!.Resources!.Add
            (
                new Resource
                {
                    Id = "aa",
                    Name = data.name,
                    DisplayName = data.displayName,
                    Url = data.url,
                    Description = data.description,
                    Enabled = 1,
                    CreatedDate = data.createdDate,
                    UpdatedDate = data.updatedDate,
                    CreatedUser = data.createdUser,
                    UpdatedUser = data.updatedUser
                }
            );
            context.SaveChanges();
            return Results.Created($"/resource/{data.name}", data);
        }
        else
        {
            var hasChanges = false;

            // Apply update to only changed fields.

            ModuleHelper.PreUpdate(data.url, existingRecord.Url, ref hasChanges);
            ModuleHelper.PreUpdate(data.displayName, existingRecord.DisplayName, ref hasChanges);
            ModuleHelper.PreUpdate(data.description, existingRecord.Description, ref hasChanges);
            ModuleHelper.PreUpdate(data.enabled.ToString(), existingRecord.Enabled.ToString(), ref hasChanges);

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
     [FromRoute(Name = "resourceName")] string resourceName,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.Resources?.FirstOrDefault(t => t.Name == resourceName);

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

    static IResult searchResource(
      [FromQuery(Name = "resourceName")] string resourceName,
      [FromServices] ResourceDBContext context
      )
    {
        var resource = context!.Resources!
            .Where(t => t.Name!.Contains(resourceName));

        if (resource.ToList().Count == 0)
            return Results.NotFound();

        return Results.Ok(
            resource.ToArray()
        );
    }

    static IResult getResource(
    [FromRoute(Name = "resourceName")] string resourceName,
    [FromServices] ResourceDBContext context
    )
    {
        var resource = context!.Resources!
            .FirstOrDefault(t => t.Name == resourceName);

        if (resource == null)
            return Results.NotFound();

        return Results.Ok(resource);
    }
}