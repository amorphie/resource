using Microsoft.AspNetCore.Mvc;
public static class RoleGroupModule
{
    public static void MapRoleGroupEndpoints(this WebApplication app)
    {
        //getAllRoleGroups
        app.MapGet("/roleGroup", getAllRoleGroups)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns all role groups.";
                operation.Parameters[0].Description = "Paging parameter. **limit** is the page size of resultset.";
                operation.Parameters[1].Description = "Paging parameter. **Token** is returned from last query.";
                return operation;
            })
         .Produces<GetRoleGroupResponse[]>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);

        // //searchRoleGroup
        // app.MapGet("/roleGroup", searchRoleGroup)
        //     .WithOpenApi(operation =>
        //         {
        //             operation.Summary = "Returns queried role groups.";
        //             operation.Parameters[0].Description = "Full or partial name of role group name to be queried.";
        //             return operation;
        //         })
        //     .Produces<GetRoleGroupResponse[]>(StatusCodes.Status200OK)
        //     .Produces(StatusCodes.Status204NoContent);

        //getRoleGroup
        app.MapGet("/roleGroup/{roleGroupId}", getRoleGroup)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns requested role group.";
                operation.Parameters[0].Description = "Id of the requested role group.";
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
        app.MapDelete("/roleGroup/{roleGroupId}", deleteRoleGroup)
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
        var existingRecord = context?.RoleGroups?.FirstOrDefault(t => t.Id == data.id);

        if (existingRecord == null)
        {
            context!.RoleGroups!.Add
            (
                new RoleGroup
                {
                    Id = data.id,
                    Title = data.title,
                    Tags = data.tags,
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
            return Results.Created($"/roleGroup/{data.id}", data);
        }
        else
        {
            var hasChanges = false;

            // Apply update to only changed fields.

            ModuleHelper.PreUpdate(data.title.ToString(), existingRecord.Title!.ToString(), ref hasChanges);
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

    static IResult deleteRoleGroup(
     [FromRoute(Name = "roleGroupId")] Guid roleGroupId,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.RoleGroups?.FirstOrDefault(t => t.Id == roleGroupId);

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
    [FromQuery(Name = "roleGroupTitle")] string roleGroupTitle,
    [FromServices] ResourceDBContext context
    )
    {
        var roleGroups = context!.RoleGroups!
            .Where(t => t.Title!.Contains(roleGroupTitle));

        if (roleGroups.ToList().Count == 0)
            return Results.NotFound();

        return Results.Ok(
            roleGroups.ToArray()
        );
    }

    static IResult getRoleGroup(
    [FromRoute(Name = "roleGroupId")] Guid roleGroupId,
    [FromServices] ResourceDBContext context
    )
    {
        var roleGroup = context!.RoleGroups!
            .FirstOrDefault(t => t.Id == roleGroupId);

        if (roleGroup == null)
            return Results.NotFound();

        return Results.Ok(roleGroup);
    }

    static IResult getAllRoleGroups(
        [FromServices] ResourceDBContext context,
        [FromQuery][Range(0, 100)] int page = 0,
        [FromQuery][Range(5, 100)] int pageSize = 100
        )
    {
        var query = context!.RoleGroups!
            // .Include(t => t.Tags)
            .Skip(page * pageSize)
            .Take(pageSize);
       
        var roleGroups = query.ToList();

        if (roleGroups.Count() > 0)
        {
             return Results.Ok(roleGroups.Select(roleGroup =>
              new GetRoleGroupResponse(
               roleGroup.Id,
               roleGroup.Title,
               roleGroup.Tags,
               roleGroup.Status,
               roleGroup.CreatedAt,
               roleGroup.ModifiedAt,
               roleGroup.CreatedBy,
               roleGroup.ModifiedBy,
               roleGroup.CreatedByBehalfOf,
               roleGroup.ModifiedByBehalfOf
               )
            ).ToArray());
        }
        else
            return Results.NoContent();
    }
}

