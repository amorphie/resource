using amorphie.core.Base;
using amorphie.core.Enums;
using amorphie.core.IBase;

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
                operation.Parameters[2].Description = "RFC 5646 compliant language code.";
                return operation;
            })
         .Produces<DtoRoleGroup>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);

        //saveRoleGroup
        app.MapPost("/roleGroup", saveRoleGroup)
       .WithTopic("pubsub", "SaveRoleGroup")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested role group.";
                    return operation;
                })
                .Produces<DtoRoleGroup>(StatusCodes.Status200OK)
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

    static IResponse<List<DtoRoleGroup>> getAllRoleGroups(
        [FromServices] ResourceDBContext context,
        [FromQuery][Range(0, 100)] int page = 0,
        [FromQuery][Range(5, 100)] int pageSize = 100,
        [FromHeader(Name = "Language")] string? language = "en-EN"
        )
    {
        var roleGroups = context!.RoleGroups!
            .Include(t => t.Titles.Where(t => t.Language == language))
            .Skip(page * pageSize).Take(pageSize)
            .AsQueryable().ToList();

        if (roleGroups.Count == 0)
        {
            return new Response<List<DtoRoleGroup>>
            {
                Data = null,
                Result = new amorphie.core.Base.Result(Status.Success, "Veri bulunamadı")
            };
        }

        return new Response<List<DtoRoleGroup>>
        {
            Data = roleGroups.Select(x => ObjectMapper.Mapper.Map<DtoRoleGroup>(x)).ToList(),
            Result = new amorphie.core.Base.Result(Status.Success, "Getirme başarılı")
        };
    }

    static IResponse<DtoRoleGroup> saveRoleGroup(
        [FromBody] DtoSaveRoleGroupRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        var existingRecord = context?.RoleGroups?.FirstOrDefault(t => t.Id == data.Id);

        if (existingRecord == null)
        {
            var roleGroup = ObjectMapper.Mapper.Map<RoleGroup>(data);
            roleGroup.CreatedAt = DateTime.UtcNow;
            context!.RoleGroups!.Add(roleGroup);
            context.SaveChanges();

            return new Response<DtoRoleGroup>
            {
                Data = ObjectMapper.Mapper.Map<DtoRoleGroup>(roleGroup),
                Result = new amorphie.core.Base.Result(Status.Success, "Kaydedildi")
            };
        }
        else
        {
            if (CheckForUpdate(data, existingRecord))
            {
                context!.SaveChanges();

                return new Response<DtoRoleGroup>
                {
                    Data = ObjectMapper.Mapper.Map<DtoRoleGroup>(existingRecord),
                    Result = new amorphie.core.Base.Result(Status.Success, "Güncelleme Başarili")
                };
            }
        }
        return new Response<DtoRoleGroup>
        {
            Data = ObjectMapper.Mapper.Map<DtoRoleGroup>(existingRecord),
            Result = new Result(Status.Error, "Değişiklik yok")
        };
    }

    static bool CheckForUpdate(DtoSaveRoleGroupRequest data, RoleGroup existingRecord)
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

    static IResponse deleteRoleGroup(
     [FromRoute(Name = "roleGroupId")] Guid roleGroupId,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.RoleGroups?.FirstOrDefault(t => t.Id == roleGroupId);

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

