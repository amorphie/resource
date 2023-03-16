using amorphie.core.Base;
using amorphie.core.Enums;
using amorphie.core.IBase;

public static class RoleGroupRoleModule
{
    public static void MapRoleGroupRoleEndpoints(this WebApplication app)
    {
        //getAllRoleGroupRoles
        app.MapGet("/roleGroupRole", getAllRoleGroupRoles)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns all role group roles.";
                operation.Parameters[0].Description = "Paging parameter. **limit** is the page size of resultset.";
                operation.Parameters[1].Description = "Paging parameter. **Token** is returned from last query.";
                return operation;
            })
         .Produces<DtoRoleGroupRole>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);

        //saveRoleGroupRole
        app.MapPost("/roleGroupRole", saveRoleGroupRole)
       .WithTopic("pubsub", "SaveRoleGroupRole")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested role group role.";
                    return operation;
                })
                .Produces<DtoRoleGroupRole>(StatusCodes.Status200OK)
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

    static IResponse<List<DtoRoleGroupRole>> getAllRoleGroupRoles(
         [FromServices] ResourceDBContext context,
         [FromQuery][Range(0, 100)] int page = 0,
         [FromQuery][Range(5, 100)] int pageSize = 100
         )
    {
        var roleGroupRoles = context!.RoleGroupRoles!
            .Skip(page * pageSize).Take(pageSize)
            .AsQueryable().ToList();

        if (roleGroupRoles.Count == 0)
        {
            return new Response<List<DtoRoleGroupRole>>
            {
                Data = null,
                Result = new amorphie.core.Base.Result(Status.Success, "Veri bulunamadı")
            };
        }

        return new Response<List<DtoRoleGroupRole>>
        {
            Data = roleGroupRoles.Select(x => ObjectMapper.Mapper.Map<DtoRoleGroupRole>(x)).ToList(),
            Result = new amorphie.core.Base.Result(Status.Success, "Getirme başarılı")
        };
    }

    static IResponse<DtoRoleGroupRole> saveRoleGroupRole(
        [FromBody] DtoSaveRoleGroupRoleRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        if (data.Id == null)
        {
            var roleGroupRole = ObjectMapper.Mapper.Map<RoleGroupRole>(data);
            roleGroupRole.Id = Guid.NewGuid();
            roleGroupRole.CreatedAt = DateTime.UtcNow;
            context!.RoleGroupRoles!.Add(roleGroupRole);
            context.SaveChanges();

            return new Response<DtoRoleGroupRole>
            {
                Data = ObjectMapper.Mapper.Map<DtoRoleGroupRole>(roleGroupRole),
                Result = new amorphie.core.Base.Result(Status.Success, "Kaydedildi")
            };
        }
        else
        {
            var existingRecord = context?.RoleGroupRoles?.FirstOrDefault(t => t.Id == data.Id);

            if (CheckForUpdate(data, existingRecord!))
            {
                context!.SaveChanges();

                return new Response<DtoRoleGroupRole>
                {
                    Data = ObjectMapper.Mapper.Map<DtoRoleGroupRole>(existingRecord),
                    Result = new amorphie.core.Base.Result(Status.Success, "Güncelleme Başarili")
                };
            }

            return new Response<DtoRoleGroupRole>
            {
                Data = ObjectMapper.Mapper.Map<DtoRoleGroupRole>(existingRecord),
                Result = new Result(Status.Error, "Değişiklik yok")
            };
        }
    }

    static bool CheckForUpdate(DtoSaveRoleGroupRoleRequest data, RoleGroupRole existingRecord)
    {
        var hasChanges = false;

        if (data.Status != null && data.Status != existingRecord.Status)
        {
            existingRecord.Status = data.Status;
            hasChanges = true;
        }

        if (hasChanges)
        {
            existingRecord.ModifiedAt = DateTime.Now.ToUniversalTime();
            return true;
        }
        else
        {
            return false;
        }
    }
    static IResponse deleteRoleGroupRole(
    [FromRoute(Name = "roleGroupRoleId")] Guid roleGroupRoleId,
    [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.RoleGroupRoles?.FirstOrDefault(t => t.Id == roleGroupRoleId);

        if (existingRecord == null)
        {
            return new Response
            {
                Result = new amorphie.core.Base.Result(Status.Error, "Kayıt bulunumadı")
            };
        }
        else
        {
            context!.Remove(existingRecord);
            context.SaveChanges();

            return new Response
            {
                Result = new amorphie.core.Base.Result(Status.Error, "Silme başarılı")
            };
        }
    }
}