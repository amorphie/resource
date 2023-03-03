using amorphie.core.Base;
using amorphie.core.Enums;
using amorphie.core.IBase;
using Result = amorphie.core.Base.Result;

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
         .Produces<DtoPrivilege>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);

        //getPrivilege
        app.MapGet("/privilege/{privilegeId}", getPrivilege)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns requested privilege.";
                operation.Parameters[0].Description = "Id of the requested privilege.";
                return operation;
            })
            .Produces<DtoPrivilege>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        //savePrivilege
        app.MapPost("/privilege", savePrivilege)
       .WithTopic("pubsub", "SavePrivilege")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested privilege.";
                    return operation;
                })
                .Produces<DtoPrivilege>(StatusCodes.Status200OK)
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

    static IResponse<List<DtoPrivilege>> getAllPrivileges(
       [FromServices] ResourceDBContext context,
       [FromQuery][Range(0, 100)] int page = 0,
       [FromQuery][Range(5, 100)] int pageSize = 100
       )
    {
        var privileges = context!.Privileges!
            .Skip(page * pageSize).Take(pageSize)
            .AsQueryable().ToList();

        if (privileges.Count == 0)
        {
            return new Response<List<DtoPrivilege>>
            {
                Data = null,
                Result = new amorphie.core.Base.Result(Status.Success, "Veri bulunamadı")
            };
        }

        return new Response<List<DtoPrivilege>>
        {
            Data = privileges.Select(x => ObjectMapper.Mapper.Map<DtoPrivilege>(x)).ToList(),
            Result = new amorphie.core.Base.Result(Status.Success, "Getirme başarılı")
        };
    }

    static IResponse<DtoPrivilege> getPrivilege(
        [FromRoute(Name = "privilegeId")] Guid privilegeId,
        [FromServices] ResourceDBContext context
        )
    {
        var privilege = context!.Privileges!
            .FirstOrDefault(t => t.Id == privilegeId);

        if (privilege == null)
        {
            return new Response<DtoPrivilege>
            {
                Data = null,
                Result = new amorphie.core.Base.Result(Status.Success, "Veri bulunamadı")
            };
        }

        return new Response<DtoPrivilege>
        {
            Data = ObjectMapper.Mapper.Map<DtoPrivilege>(privilege),
            Result = new amorphie.core.Base.Result(Status.Success, "Getirme başarılı")
        };
    }

    static IResponse<DtoPrivilege> savePrivilege(
        [FromBody] DtoSavePrivilegeRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        var existingRecord = context?.Privileges?.FirstOrDefault(t => t.Id == data.Id);

        if (existingRecord == null)
        {
            var privilege = ObjectMapper.Mapper.Map<Privilege>(data);
            privilege.CreatedAt = DateTime.UtcNow;
            context!.Privileges!.Add(privilege);
            context.SaveChanges();

            return new Response<DtoPrivilege>
            {
                Data = ObjectMapper.Mapper.Map<DtoPrivilege>(privilege),
                Result = new amorphie.core.Base.Result(Status.Success, "Kaydedildi")
            };
        }
        else
        {
            if (CheckForUpdate(data, existingRecord))
            {
                context!.SaveChanges();

                return new Response<DtoPrivilege>
                {
                    Data = ObjectMapper.Mapper.Map<DtoPrivilege>(existingRecord),
                    Result = new amorphie.core.Base.Result(Status.Success, "Güncelleme Başarili")
                };
            }
        }
        return new Response<DtoPrivilege>
        {
            Data = ObjectMapper.Mapper.Map<DtoPrivilege>(existingRecord),
            Result = new Result(Status.Error, "Değişiklik yok")
        };
    }

    static bool CheckForUpdate(DtoSavePrivilegeRequest data, Privilege existingRecord)
    {
        var hasChanges = false;

        if (data.Ttl != null && data.Ttl != existingRecord.Ttl)
        {
            existingRecord.Ttl = data.Ttl.Value;
            hasChanges = true;
        }

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

    static IResponse deletePrivilege(
     [FromRoute(Name = "privilegeId")] Guid privilegeId,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.Privileges?.FirstOrDefault(t => t.Id == privilegeId);

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
