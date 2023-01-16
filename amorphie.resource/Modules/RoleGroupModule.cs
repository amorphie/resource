using Microsoft.AspNetCore.Mvc;
public static class RoleGroupModule
{
    public static void MapRoleGroupEndpoints(this WebApplication app)
    {
        //searchRoleGroup
        app.MapGet("/roleGroup", searchRoleGroup)
            .WithOpenApi(operation =>
                {
                    operation.Summary = "Returns queried role groups.";
                    operation.Parameters[0].Description = "Full or partial name of role group name to be queried.";
                    return operation;
                })
            .Produces<GetRoleGroupResponse[]>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);

        //getRoleGroup
        app.MapGet("/roleGroup/{roleGroupName}", getRoleGroup)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns requested role group.";
                operation.Parameters[0].Description = "Name of the requested role group.";
                return operation;
            })
            .Produces<GetRoleGroupResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        //saveRoleGroup
        app.MapPost("/roleGroup", saveRoleGroup)
       .WithTopic("pubsub", "SaveRoleGroup")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested role group.";
                    return operation;
                })
                .Produces<GetRoleGroupResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);

        //deleteRoleGroup
        app.MapDelete("/roleGroup/{roleGroupName}", deleteRoleGroup)
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Deletes existing role group.";
                    return operation;
                })
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent);
    }

    static IResult saveRoleGroup(
        [FromBody] SaveRoleGroupRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        var existingRecord = context?.RoleGroups?.FirstOrDefault(t => t.Name == data.name);

        if (existingRecord == null)
        {
            context!.RoleGroups!.Add
            (
                new RoleGroup
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
            return Results.Created($"/roleGroup/{data.name}", data);
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

    static IResult deleteRoleGroup(
     [FromRoute(Name = "roleGroupName")] string roleGroupName,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.RoleGroups?.FirstOrDefault(t => t.Name == roleGroupName);

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


    static IResult searchRoleGroup(
    [FromQuery(Name = "roleGroupName")] string roleGroupName,
    [FromServices] ResourceDBContext context
    )
    {
        var roleGroups = context!.RoleGroups!
            .Where(t => t.Name!.Contains(roleGroupName));

        if (roleGroups.ToList().Count == 0)
            return Results.NotFound();

        return Results.Ok(
            roleGroups.ToArray()
        );
    }

    static IResult getRoleGroup(
    [FromRoute(Name = "roleGroupName")] string roleGroupName,
    [FromServices] ResourceDBContext context
    )
    {
        var roleGroup = context!.RoleGroups!
            .FirstOrDefault(t => t.Name == roleGroupName);

        if (roleGroup == null)
            return Results.NotFound();

        return Results.Ok(roleGroup);
    }

}
