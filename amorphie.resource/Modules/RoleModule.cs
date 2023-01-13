using Microsoft.AspNetCore.Mvc;
public static class RoleModule
{
    public static void MapRoleEndpoints(this WebApplication app)
    {
        //searchRole
        app.MapGet("/role", searchRole)
            .WithOpenApi(operation =>
                {
                    operation.Summary = "Returns queried roles.";
                    operation.Parameters[0].Description = "Full or partial name of role name to be queried.";
                    return operation;
                })
            .Produces<GetRoleResponse[]>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);

        //getRole
        app.MapGet("/role/{roleName}", getRole)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns requested role.";
                operation.Parameters[0].Description = "Name of the requested role.";
                return operation;
            })
            .Produces<GetRoleResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        //saveRole
        app.MapPost("/role", saveRole)
       .WithTopic("pubsub", "SaveRole")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested role.";
                    return operation;
                })
                .Produces<GetRoleResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);

        //deleteRole
        app.MapDelete("/role/{roleName}", deleteRole)
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Deletes existing role.";
                    return operation;
                })
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent);
    }

    static IResult saveRole(
        [FromBody] SaveRoleRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        var existingRecord = context?.Roles?.FirstOrDefault(t => t.Name == data.name);

        if (existingRecord == null)
        {
            context!.Roles!.Add
            (
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = data.name,
                    Enabled = 1,
                    CreatedDate = data.createdDate,
                    UpdatedDate = data.updatedDate,
                    CreatedUser = data.createdUser,
                    UpdatedUser = data.updatedUser
                }
            );
            context.SaveChanges();
            return Results.Created($"/role/{data.name}", data);
        }
        else
        {
            var hasChanges = false;

            // Apply update to only changed fields.

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

    static IResult deleteRole(
     [FromRoute(Name = "roleName")] string roleName,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.Roles?.FirstOrDefault(t => t.Name == roleName);

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


    static IResult searchRole(
    [FromQuery(Name = "roleName")] string roleName,
    [FromServices] ResourceDBContext context
    )
    {
        var roles = context!.Roles!
            .Where(t => t.Name!.Contains(roleName));

        if (roles.ToList().Count == 0)
            return Results.NotFound();

        return Results.Ok(
            roles.ToArray()
        );
    }

    static IResult getRole(
    [FromRoute(Name = "roleName")] string roleName,
    [FromServices] ResourceDBContext context
    )
    {
        var role = context!.Roles!
            .FirstOrDefault(t => t.Name == roleName);

        if (role == null)
            return Results.NotFound();

        return Results.Ok(role);
    }

}
