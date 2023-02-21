public static class PrivilegeModule
{
    public static void MapPrivilegeEndpoints(this WebApplication app)
    {
        //getAllPrivileges
        app.MapGet("/privilege", getAllPrivileges)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns all privileges.";
                operation.Parameters[0].Description = "Paging parameter. **limit** is the page size of resultset.";
                operation.Parameters[1].Description = "Paging parameter. **Token** is returned from last query.";
                return operation;
            })
         .Produces<GetPrivilegeResponse[]>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);

        //getPrivilege
        app.MapGet("/privilege/{privilegeId}", getPrivilege)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns requested privilege.";
                operation.Parameters[0].Description = "Id of the requested privilege.";
                return operation;
            })
            .Produces<GetPrivilegeResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        //savePrivilege
        app.MapPost("/privilege", savePrivilege)
       .WithTopic("pubsub", "SavePrivilege")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested privilege.";
                    return operation;
                })
                .Produces<GetPrivilegeResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);

        //deletePrivilege
        app.MapDelete("/privilege/{privilegeId}", deletePrivilege)
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Deletes existing privilege.";
                    return operation;
                })
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent);
    }

    static IResult savePrivilege(
        [FromBody] SavePrivilegeRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        var existingRecord = context?.Privileges?.FirstOrDefault(t => t.Id == data.id);

        if (existingRecord == null)
        {
            context!.Privileges!.Add
            (
                new Privilege
                {
                    Id = data.id,
                    ResourceId = data.resourceId,
                    Url = data.url,
                    Ttl = data.ttl,
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
            return Results.Created($"/privilege/{data.id}", data);
        }
        else
        {
            var hasChanges = false;

            // Apply update to only changed fields.

            ModuleHelper.PreUpdate(data.url, existingRecord.Url, ref hasChanges);
            ModuleHelper.PreUpdate(data.ttl.ToString(), existingRecord.Ttl.ToString(), ref hasChanges);
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

    static IResult deletePrivilege(
     [FromRoute(Name = "privilegeId")] Guid privilegeId,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.Privileges?.FirstOrDefault(t => t.Id == privilegeId);

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

    static IResult getPrivilege(
    [FromRoute(Name = "privilegeId")] Guid privilegeId,
    [FromServices] ResourceDBContext context
    )
    {
        var privilege = context!.Privileges!
            .FirstOrDefault(t => t.Id == privilegeId);

        if (privilege == null)
            return Results.NotFound();

        return Results.Ok(
             new GetPrivilegeResponse(
              privilege.Id,
              privilege.ResourceId,
              privilege.Url,
              privilege.Ttl,
              privilege.Status,
              privilege.CreatedAt,
              privilege.ModifiedAt,
              privilege.CreatedBy,
              privilege.ModifiedBy,
              privilege.CreatedByBehalfOf,
              privilege.ModifiedByBehalfOf
              )
           );
    }

    static IResult getAllPrivileges(
        [FromServices] ResourceDBContext context,
        [FromQuery][Range(0, 100)] int page = 0,
        [FromQuery][Range(5, 100)] int pageSize = 100
        )
    {
        var query = context!.Privileges!
            // .Include(t => t.Tags)
            .Skip(page * pageSize)
            .Take(pageSize);

        var privileges = query.ToList();

        if (privileges.Count() > 0)
        {
            return Results.Ok(privileges.Select(privilege =>
             new GetPrivilegeResponse(
              privilege.Id,
              privilege.ResourceId,
              privilege.Url,
              privilege.Ttl,
              privilege.Status,
              privilege.CreatedAt,
              privilege.ModifiedAt,
              privilege.CreatedBy,
              privilege.ModifiedBy,
              privilege.CreatedByBehalfOf,
              privilege.ModifiedByBehalfOf
              )
           ).ToArray());
        }
        else
            return Results.NoContent();
    }

}
