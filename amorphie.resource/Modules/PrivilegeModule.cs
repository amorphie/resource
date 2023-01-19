using Microsoft.AspNetCore.Mvc;
public static class PrivilegeModule
{
    public static void MapPrivilegeEndpoints(this WebApplication app)
    {
        //searchPrivilege
        app.MapGet("/privilege", searchPrivilege)
            .WithOpenApi(operation =>
                {
                    operation.Summary = "Returns queried privileges.";
                    operation.Parameters[0].Description = "Full or partial name of privilege name to be queried.";
                    return operation;
                })
            .Produces<GetPrivilegeResponse[]>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);

        //getPrivilege
        app.MapGet("/privilege/{privilegeName}", getPrivilege)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns requested privilege.";
                operation.Parameters[0].Description = "Name of the requested privilege.";
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
        app.MapDelete("/privilege/{privilegeName}", deletePrivilege)
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
        var existingRecord = context?.Privileges?.FirstOrDefault(t => t.Name == data.name);

        if (existingRecord == null)
        {
            context!.Privileges!.Add
            (
                new Privilege
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
            return Results.Created($"/privilege/{data.name}", data);
        }
        else
        {
            var hasChanges = false;

            // Apply update to only changed fields.

            ModuleHelper.PreUpdate(data.name, existingRecord.Name, ref hasChanges);
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

    static IResult deletePrivilege(
     [FromRoute(Name = "privilegeName")] string privilegeName,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.Privileges?.FirstOrDefault(t => t.Name == privilegeName);

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

    static IResult searchPrivilege(
    [FromQuery(Name = "privilegeName")] string privilegeName,
    [FromServices] ResourceDBContext context
    )
    {
        var privileges = context!.Privileges!
            .Where(t => t.Name!.Contains(privilegeName));

        if (privileges.ToList().Count == 0)
            return Results.NotFound();

        return Results.Ok(
            privileges.ToArray()
        );
    }

    static IResult getPrivilege(
    [FromRoute(Name = "privilegeName")] string privilegeName,
    [FromServices] ResourceDBContext context
    )
    {
        var privilege = context!.Privileges!
            .FirstOrDefault(t => t.Name == privilegeName);

        if (privilege == null)
            return Results.NotFound();

        return Results.Ok(privilege);
    }

}