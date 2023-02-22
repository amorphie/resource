public static class RoleModule
{
    public static void MapRoleEndpoints(this WebApplication app)
    {
        //getAllRoles
        app.MapGet("/role", getAllRoles)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns all roles.";
                operation.Parameters[0].Description = "Paging parameter. **limit** is the page size of resultset.";
                operation.Parameters[1].Description = "Paging parameter. **Token** is returned from last query.";
                operation.Parameters[2].Description = "RFC 5646 compliant language code.";
                return operation;
            })
         .Produces<GetRoleResponse[]>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);


        //getRole
        app.MapGet("/role/{roleId}", getRole)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns requested role.";
                operation.Parameters[0].Description = "Id of the requested role.";
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
        app.MapDelete("/role/{roleId}", deleteRole)
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
        var existingRecord = context?.Roles?.FirstOrDefault(t => t.Id == data.id);

        if (existingRecord == null)
        {
            context!.Roles!.Add
            (
                new Role
                {
                    Id = data.id,
                    Titles = data.titles,
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
            return Results.Created($"/role/{data.id}", data);
        }
        else
        {
            var hasChanges = false;

            // Apply update to only changed fields.

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

    static IResult deleteRole(
     [FromRoute(Name = "roleId")] Guid roleId,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.Roles?.FirstOrDefault(t => t.Id == roleId);

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

    static IResult getRole(
    [FromRoute(Name = "roleId")] Guid roleId,
    [FromServices] ResourceDBContext context
    )
    {
        var role = context!.Roles!.Include(t => t.Titles)
            .FirstOrDefault(t => t.Id == roleId);

        if (role == null)
            return Results.NotFound();

        return Results.Ok(
              new GetRoleResponse(
               role.Id,
               role.Titles.ToArray(),
               role.Tags,
               role.Status,
               role.CreatedAt,
               role.ModifiedAt,
               role.CreatedBy,
               role.ModifiedBy,
               role.CreatedByBehalfOf,
               role.ModifiedByBehalfOf
               )
            );
    }

    static IResult getAllRoles(
        [FromServices] ResourceDBContext context,
        [FromQuery][Range(0, 100)] int page = 0,
        [FromQuery][Range(5, 100)] int pageSize = 100,
        [FromHeader(Name = "Language")] string? language = "en-EN"
        )
    {
        var query = context!.Roles!
            .Include(t => t.Titles.Where(t => t.Language == language))
            .Skip(page * pageSize)
            .Take(pageSize);

        var roles = query.ToList();

        if (roles.Count() > 0)
        {
            return Results.Ok(roles.Select(role =>
             new GetRoleResponse(
              role.Id,
              role.Titles.ToArray(),
              role.Tags,
              role.Status,
              role.CreatedAt,
              role.ModifiedAt,
              role.CreatedBy,
              role.ModifiedBy,
              role.CreatedByBehalfOf,
              role.ModifiedByBehalfOf
              )
           ).ToArray());
        }
        else
            return Results.NoContent();
    }
}
