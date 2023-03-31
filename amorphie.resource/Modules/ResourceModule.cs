using amorphie.core.Base;
using amorphie.core.Enums;
using amorphie.core.IBase;

public static class ResourceModule
{
    public static void MapResourceEndpoints(this WebApplication app)
    {
        //getAllResources
        app.MapGet("/resource", getAllResources)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns all resources.";
                operation.Parameters[0].Description = "Paging parameter. **limit** is the page size of resultset.";
                operation.Parameters[1].Description = "Paging parameter. **Token** is returned from last query.";
                operation.Parameters[2].Description = "RFC 5646 compliant language code.";
                return operation;
            })
         .Produces<DtoResource>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);

        //getResource
        app.MapGet("/resource/{resourceId}", getResource)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns requested resource.";
                operation.Parameters[0].Description = "Id of the requested resource.";
                return operation;
            })
            .Produces<DtoResource>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);


        //saveResource
        app.MapPost("/resource", saveResource)
       .WithTopic("pubsub", "saveResource")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested resource.";
                    return operation;
                })
                .Produces<DtoResource>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);

        //deleteResource
        app.MapDelete("/resource/{resourceId}", deleteResource)
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Deletes existing resource.";
                    return operation;
                })
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent);
    }

    static IResponse<List<DtoResource>> getAllResources(
       [FromServices] ResourceDBContext context,
       [FromQuery][Range(0, 100)] int page = 0,
       [FromQuery][Range(5, 100)] int pageSize = 100,
       [FromHeader(Name = "Language")] string? language = "en-EN"
       )
    {
        var resources = context!.Resources!
            .Include(t => t.DisplayNames.Where(t => t.Language == language))
            .Include(t => t.Descriptions.Where(t => t.Language == language))
            .Skip(page * pageSize).Take(pageSize)
            .AsQueryable().ToList();

        if (resources.Count == 0)
        {
            return new Response<List<DtoResource>>
            {
                Data = null,
                Result = new amorphie.core.Base.Result(Status.Success, "Veri bulunamadı")
            };
        }

        return new Response<List<DtoResource>>
        {
            Data = resources.Select(x => ObjectMapper.Mapper.Map<DtoResource>(x)).ToList(),
            Result = new amorphie.core.Base.Result(Status.Success, "Getirme başarılı")
        };
    }

    static IResponse<DtoResource> getResource(
        [FromRoute(Name = "resourceId")] Guid resourceId,
        [FromServices] ResourceDBContext context
        )
    {
        var resource = context!.Resources!
            .FirstOrDefault(t => t.Id == resourceId);

        if (resource == null)
        {
            return new Response<DtoResource>
            {
                Data = null,
                Result = new amorphie.core.Base.Result(Status.Success, "Veri bulunamadı")
            };
        }

        return new Response<DtoResource>
        {
            Data = ObjectMapper.Mapper.Map<DtoResource>(resource),
            Result = new amorphie.core.Base.Result(Status.Success, "Getirme başarılı")
        };
    }

    static IResponse<DtoResource> saveResource(
        [FromBody] DtoSaveResourceRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        Resource? existingRecord = null;

        if (data.Id == null)
        {
            data.Id = Guid.NewGuid();
        }
        else
        {
            existingRecord = context?.Resources!.FirstOrDefault(t => t.Id == data.Id);
        }

        if (existingRecord == null)
        {
            var resource = ObjectMapper.Mapper.Map<Resource>(data);
            resource.CreatedAt = DateTime.UtcNow;
            context!.Resources!.Add(resource);
            context.SaveChanges();

            return new Response<DtoResource>
            {
                Data = ObjectMapper.Mapper.Map<DtoResource>(resource),
                Result = new amorphie.core.Base.Result(Status.Success, "Kaydedildi")
            };
        }
        else
        {
            if (CheckForUpdate(data, existingRecord!))
            {
                context!.SaveChanges();

                return new Response<DtoResource>
                {
                    Data = ObjectMapper.Mapper.Map<DtoResource>(existingRecord),
                    Result = new amorphie.core.Base.Result(Status.Success, "Güncelleme Başarili")
                };
            }

            return new Response<DtoResource>
            {
                Data = ObjectMapper.Mapper.Map<DtoResource>(existingRecord),
                Result = new Result(Status.Error, "Değişiklik yok")
            };
        }
    }

    static bool CheckForUpdate(DtoSaveResourceRequest data, Resource existingRecord)
    {
        var hasChanges = false;

        if (data.Type != existingRecord.Type)
        {
            existingRecord.Type = data.Type;
            hasChanges = true;
        }

        if (data.Url != null && data.Url != existingRecord.Url)
        {
            existingRecord.Url = data.Url;
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

    static IResponse deleteResource(
     [FromRoute(Name = "resourceId")] Guid resourceId,
     [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.Resources?.FirstOrDefault(t => t.Id == resourceId);

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