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
         .Produces<GetResourceResponse>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);

        //savePrivilege
        app.MapPost("/resource", saveResource)
       .WithTopic("pubsub", "saveResource")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested resource.";
                    return operation;
                })
                .Produces<GetResourceResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);
    }

    static IResponse<List<DtoResource>> getAllResources(
       [FromServices] ResourceDBContext context,
       [FromQuery][Range(0, 100)] int page = 0,
       [FromQuery][Range(5, 100)] int pageSize = 100,
       [FromHeader(Name = "Language")] string? language = "en-EN"
       )
    {
        var result = new List<GetResourceResponse>();
        var queryAfterWhere = new List<Resource>();
        var query = context!.Resources!
            .Include(t => t.DisplayNames.Where(t => t.Language == language))
            .Include(t => t.Descriptions.Where(t => t.Language == language))
            .Skip(page * pageSize).Take(pageSize)
            .AsQueryable().ToList();

        queryAfterWhere = query.ToList();

        var resources = queryAfterWhere.ToList();

        foreach (var item in resources)
        {
            result.Add(
               new GetResourceResponse
               (
                        item.Id,
                        item.DisplayNames.ToArray(),
                        item.Type,
                        item.Url,
                        item.Descriptions.ToArray(),
                        item.Tags,
                        item.Status,
                        item.CreatedAt,
                        item.ModifiedAt,
                        item.CreatedBy,
                        item.ModifiedBy,
                        item.CreatedByBehalfOf,
                        item.ModifiedByBehalfOf
              )
                    );
        }

        // if (result.Count == 0)
        //      return Results.NoContent();


         return new Response<List<DtoResource>>
            {
                Data = ObjectMapper.Mapper.Map<List<DtoResource>>(result),
                Result = new amorphie.core.Base.Result(Status.Success, "Getirme başarılı")
            };
    }

    static IResponse<DtoResource> saveResource(
        [FromBody] DtoSaveResourceRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        var existingRecord = context?.Resources?.FirstOrDefault(t => t.Id == data.Id);

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
            if (CheckForUpdate(data, existingRecord))
            {
                context!.SaveChanges();

                return new Response<DtoResource>
                {
                    Data = ObjectMapper.Mapper.Map<DtoResource>(existingRecord),
                    Result = new amorphie.core.Base.Result(Status.Success, "Güncelleme Başarili")
                };
            }
        }
        return new Response<DtoResource>
        {
            Data = ObjectMapper.Mapper.Map<DtoResource>(existingRecord),
            Result = new Result(Status.Error, "Değişiklik yok")
        };
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

}

