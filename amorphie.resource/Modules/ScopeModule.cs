using amorphie.core.Base;
using amorphie.core.Enums;
using amorphie.core.IBase;

public static class ScopeModule
{
    public static void MapScopeEndpoints(this WebApplication app)
    {
        //getAllScopes
        app.MapGet("/scope", getAllScopes)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns all scopes.";
                operation.Parameters[0].Description = "Paging parameter. **limit** is the page size of resultset.";
                operation.Parameters[1].Description = "Paging parameter. **Token** is returned from last query.";
                operation.Parameters[2].Description = "RFC 5646 compliant language code.";
                return operation;
            })
         .Produces<DtoScope>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);

        //getScope
        app.MapGet("/scope/{scopeId}", getScope)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns requested scope.";
                operation.Parameters[0].Description = "Id of the requested scope.";
                operation.Parameters[1].Description = "RFC 5646 compliant language code.";
                return operation;
            })
            .Produces<DtoScope>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        //saveScope
        app.MapPost("/scope", saveScope)
       .WithTopic("pubsub", "SaveScope")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested scope.";
                    return operation;
                })
                .Produces<DtoScope>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);

        //deleteScope
        app.MapDelete("/scope/{scopeId}", deleteScope)
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Deletes existing scope.";
                    return operation;
                })
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent);
    }

    static IResponse<List<DtoScope>> getAllScopes(
        [FromServices] ResourceDBContext context,
        [FromQuery][Range(0, 100)] int page = 0,
        [FromQuery][Range(5, 100)] int pageSize = 100,
        [FromHeader(Name = "Language")] string? language = "en-EN"
        )
    {
        var scopes = context!.Scopes!
            .Include(t => t.Titles.Where(t => t.Language == language))
            .Skip(page * pageSize).Take(pageSize)
            .AsQueryable().ToList();

        if (scopes.Count == 0)
        {
            return new Response<List<DtoScope>>
            {
                Data = null,
                Result = new amorphie.core.Base.Result(Status.Success, "Veri bulunamadı")
            };
        }

        return new Response<List<DtoScope>>
        {
            Data = scopes.Select(x => ObjectMapper.Mapper.Map<DtoScope>(x)).ToList(),
            Result = new amorphie.core.Base.Result(Status.Success, "Getirme başarılı")
        };
    }

    static IResponse<DtoScope> getScope(
    [FromRoute(Name = "scopeId")] Guid roleId,
    [FromServices] ResourceDBContext context,
    [FromHeader(Name = "Language")] string? language = "en-EN"
    )
    {
        var scope = context!.Scopes!
       .Include(t => t.Titles.Where(t => t.Language == language))
       .FirstOrDefault(t => t.Id == roleId);

        if (scope == null)
        {
            return new Response<DtoScope>
            {
                Data = null,
                Result = new amorphie.core.Base.Result(Status.Success, "Veri bulunamadı")
            };
        }

        return new Response<DtoScope>
        {
            Data = ObjectMapper.Mapper.Map<DtoScope>(scope),
            Result = new amorphie.core.Base.Result(Status.Success, "Getirme başarılı")
        };
    }

    static IResponse<DtoScope> saveScope(
        [FromBody] DtoSaveScopeRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        Scope? existingRecord = null;

        if (data.Id == null)
        {
            data.Id = Guid.NewGuid();
        }
        else
        {
            existingRecord = context?.Scopes!.FirstOrDefault(t => t.Id == data.Id);
        }

        if (existingRecord == null)
        {
            var scope = ObjectMapper.Mapper.Map<Scope>(data);
            scope.CreatedAt = DateTime.UtcNow;
            context!.Scopes!.Add(scope);
            context.SaveChanges();

            return new Response<DtoScope>
            {
                Data = ObjectMapper.Mapper.Map<DtoScope>(scope),
                Result = new amorphie.core.Base.Result(Status.Success, "Kaydedildi")
            };
        }
        else
        {
            if (CheckForUpdate(data, existingRecord!,context!))
            {
                context!.SaveChanges();

                return new Response<DtoScope>
                {
                    Data = ObjectMapper.Mapper.Map<DtoScope>(existingRecord),
                    Result = new amorphie.core.Base.Result(Status.Success, "Güncelleme Başarili")
                };
            }

            return new Response<DtoScope>
            {
                Data = ObjectMapper.Mapper.Map<DtoScope>(existingRecord),
                Result = new Result(Status.Error, "Değişiklik yok")
            };
        }
    }

    static bool CheckForUpdate(DtoSaveScopeRequest data, Scope existingRecord, ResourceDBContext context)
    {
        var hasChanges = false;

        if (data.Status != null && data.Status != existingRecord.Status)
        {
            existingRecord.Status = data.Status;
            hasChanges = true;
        }

        if (data.Reference != null && data.Reference != existingRecord.Status)
        {
            existingRecord.Reference = data.Reference;
            hasChanges = true;
        }

        if (data.Tags != null && data.Tags != existingRecord.Tags)
        {
            existingRecord.Tags = data.Tags;
            hasChanges = true;
        }

        foreach (MultilanguageText multilanguageText in data.Titles)
        {
            var existingTitle = existingRecord.Titles.FirstOrDefault(t => t.Language == multilanguageText.Language);

            if (existingTitle == null)
            {
                existingRecord.Titles!.Add(ObjectMapper.Mapper.Map<Translation>(multilanguageText));
                var existingTitle2 = existingRecord.Titles!.FirstOrDefault(t => t.Language == multilanguageText.Language);
                context.Add(existingTitle2);

                hasChanges = true;
            }
            else
            {
                if (existingTitle.Label != multilanguageText.Label)
                {
                    existingTitle.Label = multilanguageText.Label;
                    hasChanges = true;
                }
            }
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

    static IResponse deleteScope(
    [FromRoute(Name = "scopeId")] Guid scopeId,
    [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.Scopes?.FirstOrDefault(t => t.Id == scopeId);

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
