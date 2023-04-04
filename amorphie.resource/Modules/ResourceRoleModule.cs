using amorphie.core.Base;
using amorphie.core.Enums;
using amorphie.core.IBase;

public static class ResourceRoleModule
{
    public static void MapResourceRoleEndpoints(this WebApplication app)
    {
        //getAllResourceRoles
        app.MapGet("/resourceRole", getAllResourceRoles)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns all resource roles.";
                operation.Parameters[0].Description = "Paging parameter. **limit** is the page size of resultset.";
                operation.Parameters[1].Description = "Paging parameter. **Token** is returned from last query.";
                return operation;
            })
         .Produces<DtoResourceRole>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);

        //saveResourceRole
        app.MapPost("/resourceRole", saveResourceRole)
       .WithTopic("pubsub", "SaveResourceRole")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested resource role.";
                    return operation;
                })
                .Produces<DtoResourceRole>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);

        //deleteResourceRole
        app.MapDelete("/resourceRole/{resourceRoleId}", deleteResourceRole)
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Deletes existing resource role.";
                    return operation;
                })
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent);
    }

    static IResponse<List<DtoResourceRole>> getAllResourceRoles(
        [FromServices] ResourceDBContext context,
        [FromQuery][Range(0, 100)] int page = 0,
        [FromQuery][Range(5, 100)] int pageSize = 100
        )
    {
        var query = context!.ResourceRoles!
            .Skip(page * pageSize)
            .Take(pageSize);

        var resourceRoles = query.ToList();

        if (resourceRoles.Count == 0)
        {
            return new Response<List<DtoResourceRole>>
            {
                Data = null,
                Result = new amorphie.core.Base.Result(Status.Success, "Veri bulunamadı")
            };
        }

        return new Response<List<DtoResourceRole>>
        {
            Data = resourceRoles.Select(x => ObjectMapper.Mapper.Map<DtoResourceRole>(x)).ToList(),
            Result = new amorphie.core.Base.Result(Status.Success, "Getirme başarılı")
        };
    }

    static IResponse<DtoResourceRole> saveResourceRole(
        [FromBody] DtoSaveResourceRoleRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        ResourceRole? existingRecord = null;

        if (data.Id == null)
        {
            data.Id = Guid.NewGuid();
        }
        else
        {
            existingRecord = context?.ResourceRoles!.FirstOrDefault(t => t.Id == data.Id);
        }

        if (existingRecord == null)
        {
            var resourceRole = ObjectMapper.Mapper.Map<ResourceRole>(data);
            resourceRole.CreatedAt = DateTime.UtcNow;
            context!.ResourceRoles!.Add(resourceRole);
            context.SaveChanges();

            return new Response<DtoResourceRole>
            {
                Data = ObjectMapper.Mapper.Map<DtoResourceRole>(resourceRole),
                Result = new amorphie.core.Base.Result(Status.Success, "Kaydedildi")
            };
        }
        else
        {
            if (CheckForUpdate(data, existingRecord!))
            {
                context!.SaveChanges();

                return new Response<DtoResourceRole>
                {
                    Data = ObjectMapper.Mapper.Map<DtoResourceRole>(existingRecord),
                    Result = new amorphie.core.Base.Result(Status.Success, "Güncelleme Başarili")
                };
            }
            return new Response<DtoResourceRole>
            {
                Data = ObjectMapper.Mapper.Map<DtoResourceRole>(existingRecord),
                Result = new Result(Status.Error, "Değişiklik yok")
            };
        }
    }

    static bool CheckForUpdate(DtoResourceRole data, ResourceRole existingRecord)
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

    static IResponse deleteResourceRole(
  [FromRoute(Name = "resourceRoleId")] Guid resourceRoleId,
  [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.ResourceRoles?.FirstOrDefault(t => t.Id == resourceRoleId);

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