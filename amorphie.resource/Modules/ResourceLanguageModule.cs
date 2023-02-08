using Microsoft.AspNetCore.Mvc;

public static class ResourceLanguageModule
{
    public static void MapResourceLanguageEndpoints(this WebApplication app)
    {
        //getResource
        app.MapGet("/resourceLanguage/{resourceLanguageId}", getResourceLanguage)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns requested resource language.";
                operation.Parameters[0].Description = "Name of the requested resource language.";
                return operation;
            })
            .Produces<GetResourceLanguageResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        //saveResource
        app.MapPost("/resourceLanguage", saveResourceLanguage)
       .WithTopic("pubsub", "SaveResourceLanguage")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested resource language.";
                    return operation;
                })
                .Produces<GetResourceLanguageResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);

        //deleteResource
        app.MapDelete("/resourceLanguage/{resourceLanguageId}", deleteResourceLanguage)
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Deletes existing resource language.";
                    return operation;
                })
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent);
    }

    static IResult saveResourceLanguage(
        [FromBody] SaveResourceLanguageRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        var existingRecord = context?.ResourceLanguages?.FirstOrDefault(t => t.Id == data.id);

        if (existingRecord == null)
        {
            context!.ResourceLanguages!.Add
            (
                new ResourceLanguage
                {
                    Id = data.id,
                    TableName = data.tableName,
                    RowId = data.rowId,
                    FieldName = data.fieldName,
                    Text = data.text,
                    LanguageCode = data.languageCode,
                    Order = data.order,
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
            return Results.Created($"/resourceLanguage/{data.id}", data);
        }
        else
        {
            var hasChanges = false;

            // Apply update to only changed fields.

            ModuleHelper.PreUpdate(data.tableName, existingRecord.TableName, ref hasChanges);
            ModuleHelper.PreUpdate(data.fieldName, existingRecord.FieldName, ref hasChanges);
            ModuleHelper.PreUpdate(data.text, existingRecord.Text, ref hasChanges);
            ModuleHelper.PreUpdate(data.languageCode, existingRecord.LanguageCode, ref hasChanges);
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

    static IResult deleteResourceLanguage(
     [FromRoute(Name = "resourceLanguageId")] Guid resourceLanguageId,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.ResourceLanguages?.FirstOrDefault(t => t.Id == resourceLanguageId);

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

    static IResult getResourceLanguage(
    [FromRoute(Name = "resourceLanguageId")] Guid resourceLanguageId,
    [FromServices] ResourceDBContext context
    )
    {
        var resource = context!.ResourceLanguages!
            .FirstOrDefault(t => t.Id == resourceLanguageId);

        if (resource == null)
            return Results.NotFound();

        var resourceLanguage = context!.ResourceLanguages!
        .FirstOrDefault(t => t.Id == resourceLanguageId);

        return Results.Ok(resource);
    }
}