using Microsoft.AspNetCore.Mvc;
public static class RolePrivilegeModule
{
    public static void MapRolePrivilegeEndpoints(this WebApplication app)
    {
        //saveRolePrivilege
    //     app.MapPost("/rolePrivilege", saveRolePrivilege)
    //    .WithTopic("pubsub", "SaveRolePrivilege")
    //             .WithOpenApi(operation =>
    //             {
    //                 operation.Summary = "Saves or updates requested role privilege.";
    //                 return operation;
    //             })
    //             .Produces<GetRolePrivilegeResponse>(StatusCodes.Status200OK)
    //             .Produces(StatusCodes.Status201Created);

    //     //deleteRolePrivilege
    //     app.MapDelete("/rolePrivilege/{rolePrivilegeId}", deleteRolePrivilege)
    //             .WithOpenApi(operation =>
    //             {
    //                 operation.Summary = "Deletes existing role privilege.";
    //                 return operation;
    //             })
    //             .Produces(StatusCodes.Status404NotFound)
    //             .Produces(StatusCodes.Status204NoContent);
    // }

    // static IResult saveRolePrivilege(
    //     [FromBody] SaveRolePrivilegeRequest data,
    //     [FromServices] ResourceDBContext context
    //     )
    // {
    //     var existingRecord = context?.RolePrivileges?.FirstOrDefault(t => t.RoleId == data.RoleId && t.PrivilegeId == data.PrivilegeId);

    //     if (existingRecord == null)
    //     {
    //         var id = Guid.NewGuid();

    //         context!.RolePrivileges!.Add
    //         (
    //             new RolePrivilege
    //             {
    //                 Id = id,          
    //                 RoleId = data.RoleId,
    //                 PrivilegeId = data.PrivilegeId,          
    //                 CreatedDate = data.createdDate,
    //                 CreatedUser = data.createdUser
    //             }
    //         );
    //         context.SaveChanges();
    //         return Results.Created($"/rolePrivilege/{id}", data);
    //     }
    //     else
    //     {
    //         // var hasChanges = false;

    //         // if (hasChanges)
    //         // {
    //             context!.SaveChanges();
    //             return Results.Ok(data);
    //         // }
    //         // else
    //         // {
    //         //     return Results.Problem("Not Modified.", null, 304);
    //         // }
    //     }
    // }

    // static IResult deleteRolePrivilege(
    //  [FromRoute(Name = "rolePrivilegeId")] Guid rolePrivilegeId,
    //  [FromServices] ResourceDBContext context)
    // {
    //     var existingRecord = context?.RolePrivileges?.FirstOrDefault(t => t.Id == rolePrivilegeId);

    //     if (existingRecord == null)
    //     {
    //         return Results.NotFound();
    //     }
    //     else
    //     {
    //         context!.Remove(existingRecord);
    //         context.SaveChanges();
    //         return Results.NoContent();
    //     }
    }
}