using System.Reflection;
using Microsoft.AspNetCore.Mvc;

public static class ResourceModule
{
    public static void MapResourceEndpoints(this WebApplication app)
    {
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
                    DisplayName = data.displayName,
                    Type = data.type,
                    Url = data.url,
                    Description = data.description,
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

            ModuleHelper.PreUpdate(data.displayName, existingRecord.DisplayName, ref hasChanges);
            ModuleHelper.PreUpdate(data.url, existingRecord.Url, ref hasChanges);
            ModuleHelper.PreUpdate(data.description, existingRecord.Description, ref hasChanges);
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
        var resource = context!.Resources!
            .FirstOrDefault(t => t.Id == resourceId);

        if (resource == null)
            return Results.NotFound();

        var resourceLanguages = context!.ResourceLanguages!
        .Where(t => t.RowId == resourceId && t.LanguageCode == "tr");

        foreach (ResourceLanguage resourceLanguage in resourceLanguages)
        {
            Type type = resource.GetType();

            PropertyInfo? prop = type.GetProperty(resourceLanguage.FieldName!);

            if (prop != null)
            {
                prop.SetValue(resource, resourceLanguage.Text, null);
            }
        }

        return Results.Ok(resource);
    }

}