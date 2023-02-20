using Microsoft.AspNetCore.Mvc;
public static class ResourceRoleModule
{
    public static void MapResourceRoleEndpoints(this WebApplication app)
    {
         //getAllResourceRoles
        app.MapGet("/resourceRole", getAllResourceRoles)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns all resource roles.";
                operation.Parameters[0].Description = "Paging parameter. **limit** is the page size of resultset.";
                operation.Parameters[1].Description = "Paging parameter. **Token** is returned from last query.";
                return operation;
            })
         .Produces<GetResourceRoleResponse[]>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);

        //saveResourceRole
        app.MapPost("/resourceRole", saveResourceRole)
       .WithTopic("pubsub", "SaveResourceRole")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested resource role.";
                    return operation;
                })
                .Produces<GetResourceRoleResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);

        //deleteResourceRole
        app.MapDelete("/resourceRole/{resourceRoleId}", deleteResourceRole)
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Deletes existing resource role.";
                    return operation;
                })
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent);
    }

    static IResult saveResourceRole(
        [FromBody] SaveResourceRoleRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        var existingRecord = context?.ResourceRoles?.FirstOrDefault(t => t.Id == data.id);

        if (existingRecord == null)
        {
            context!.ResourceRoles!.Add
            (
                new ResourceRole
                {
                    Id = data.id,
                    ResourceId = data.resourceId,
                    RoleId = data.roleId,
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
            return Results.Created($"/resourceRole/{data.id}", data);
        }
        else
        {
            var hasChanges = false;

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

    static IResult deleteResourceRole(
     [FromRoute(Name = "resourceRoleId")] Guid resourceRoleId,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.ResourceRoles?.FirstOrDefault(t => t.Id == resourceRoleId);

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

    static IResult getAllResourceRoles(
        [FromServices] ResourceDBContext context,
        [FromQuery][Range(0, 100)] int page = 0,
        [FromQuery][Range(5, 100)] int pageSize = 100
        )
    {
        var query = context!.ResourceRoles!
            // .Include(t => t.Tags)
            .Skip(page * pageSize)
            .Take(pageSize);
       
        var resourceRoles = query.ToList();

        if (resourceRoles.Count() > 0)
        {
             return Results.Ok(resourceRoles.Select(res =>
              new GetResourceRoleResponse(
               res.Id,
               res.ResourceId,
               res.RoleId,
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