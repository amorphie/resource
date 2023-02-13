using Microsoft.AspNetCore.Mvc;
public static class RoleGroupRoleModule
{
    public static void MapRoleGroupRoleEndpoints(this WebApplication app)
    {
        //saveRoleGroupRole
        app.MapPost("/roleGroupRole", saveRoleGroupRole)
       .WithTopic("pubsub", "SaveRoleGroupRole")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested role group role.";
                    return operation;
                })
                .Produces<GetRoleGroupRoleResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);

        //deleteRoleGroupRole
        app.MapDelete("/roleGroupRole/{roleGroupRoleId}", deleteRoleGroupRole)
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Deletes existing role group role.";
                    return operation;
                })
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent);
    }

    static IResult saveRoleGroupRole(
        [FromBody] SaveRoleGroupRoleRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        var existingRecord = context?.RoleGroupRoles?.FirstOrDefault(t => t.RoleGroupId == data.RoleGroupId && t.RoleId == data.RoleId);

        if (existingRecord == null)
        {
            var id = Guid.NewGuid();

            context!.RoleGroupRoles!.Add
            (
                new RoleGroupRole
                {
                    Id = data.id,
                    RoleGroupId = data.RoleGroupId,
                    RoleId = data.RoleId,
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
            return Results.Created($"/roleGroupRole/{id}", data);
        }
        else
        {
            var hasChanges = false;

            ModuleHelper.PreUpdate(data.status, existingRecord.Status!.ToString(), ref hasChanges);

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

    static IResult deleteRoleGroupRole(
     [FromRoute(Name = "roleGroupRoleId")] Guid roleGroupRoleId,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.RoleGroupRoles?.FirstOrDefault(t => t.Id == roleGroupRoleId);

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