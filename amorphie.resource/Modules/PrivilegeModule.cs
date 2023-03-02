using amorphie.core.Base;
using amorphie.core.Enums;
using amorphie.core.IBase;
using Result = amorphie.core.Base.Result;

public static class PrivilegeModule
{
    public static void MapPrivilegeEndpoints(this WebApplication app)
    {
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
}
