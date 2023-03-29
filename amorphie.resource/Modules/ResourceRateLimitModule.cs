using amorphie.core.Base;
using amorphie.core.Enums;
using amorphie.core.IBase;

public static class ResourceRateLimitModule
{
    public static void MapResourceRateLimitEndpoints(this WebApplication app)
    {
        //getAllResourceRateLimits
        app.MapGet("/resourceRateLimit", getAllResourceRateLimits)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns all resource rate limits.";
                operation.Parameters[0].Description = "Paging parameter. **limit** is the page size of resultset.";
                operation.Parameters[1].Description = "Paging parameter. **Token** is returned from last query.";
                return operation;
            })
         .Produces<DtoResourceRateLimit>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);

        //saveResourceRateLimit
        app.MapPost("/resourceRateLimit", saveResourceRateLimit)
       .WithTopic("pubsub", "SaveResourceRateLimit")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested resource rate limit.";
                    return operation;
                })
                .Produces<DtoResourceRateLimit>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);

        //deleteResourceRateLimit
        app.MapDelete("/resourceRateLimit/{resourceRateLimitId}", deleteResourceRateLimit)
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Deletes existing resource rate limit.";
                    return operation;
                })
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent);
    }

    static IResponse<List<DtoResourceRateLimit>> getAllResourceRateLimits(
       [FromServices] ResourceDBContext context,
       [FromQuery][Range(0, 100)] int page = 0,
       [FromQuery][Range(5, 100)] int pageSize = 100
       )
    {
        var resourceRateLimits = context!.ResourceRateLimits!
            .Skip(page * pageSize).Take(pageSize)
            .AsQueryable().ToList();

        if (resourceRateLimits.Count == 0)
        {
            return new Response<List<DtoResourceRateLimit>>
            {
                Data = null,
                Result = new amorphie.core.Base.Result(Status.Success, "Veri bulunamadı")
            };
        }

        return new Response<List<DtoResourceRateLimit>>
        {
            Data = resourceRateLimits.Select(x => ObjectMapper.Mapper.Map<DtoResourceRateLimit>(x)).ToList(),
            Result = new amorphie.core.Base.Result(Status.Success, "Getirme başarılı")
        };
    }

    static IResponse<DtoResourceRateLimit> saveResourceRateLimit(
      [FromBody] DtoSaveResourceRateLimitRequest data,
      [FromServices] ResourceDBContext context
      )
    {
        ResourceRateLimit? existingRecord = null;

        if (data.Id == null)
        {
            data.Id = Guid.NewGuid();
        }
        else
        {
            existingRecord = context?.ResourceRateLimits?.FirstOrDefault(t => t.Id == data.Id);
        }

        if (existingRecord == null)
        {
            var resourceRateLimit = ObjectMapper.Mapper.Map<ResourceRateLimit>(data);
            resourceRateLimit.CreatedAt = DateTime.UtcNow;
            context!.ResourceRateLimits!.Add(resourceRateLimit);
            context.SaveChanges();

            return new Response<DtoResourceRateLimit>
            {
                Data = ObjectMapper.Mapper.Map<DtoResourceRateLimit>(resourceRateLimit),
                Result = new amorphie.core.Base.Result(Status.Success, "Kaydedildi")
            };
        }
        else
        {
            if (CheckForUpdate(data, existingRecord!))
            {
                context!.SaveChanges();

                return new Response<DtoResourceRateLimit>
                {
                    Data = ObjectMapper.Mapper.Map<DtoResourceRateLimit>(existingRecord),
                    Result = new amorphie.core.Base.Result(Status.Success, "Güncelleme Başarili")
                };
            }

            return new Response<DtoResourceRateLimit>
            {
                Data = ObjectMapper.Mapper.Map<DtoResourceRateLimit>(existingRecord),
                Result = new Result(Status.Error, "Değişiklik yok")
            };
        }
    }

    static bool CheckForUpdate(DtoResourceRateLimit data, ResourceRateLimit existingRecord)
    {
        var hasChanges = false;

        if (data.Scope != null && data.Scope != existingRecord.Scope)
        {
            existingRecord.Scope = data.Scope;
            hasChanges = true;
        }

        if (data.Condition != null && data.Condition != existingRecord.Condition)
        {
            existingRecord.Condition = data.Condition;
            hasChanges = true;
        }

        if (data.Cron != null && data.Cron != existingRecord.Cron)
        {
            existingRecord.Cron = data.Cron;
            hasChanges = true;
        }

        if (data.Limit != null && data.Limit != existingRecord.Limit)
        {
            existingRecord.Limit = data.Limit;
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

    static IResponse deleteResourceRateLimit(
     [FromRoute(Name = "resourceRateLimitId")] Guid resourceRateLimitId,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.ResourceRateLimits?.FirstOrDefault(t => t.Id == resourceRateLimitId);

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