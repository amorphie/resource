using amorphie.core.Base;
using amorphie.core.Enums;
using amorphie.core.IBase;

public static class RoleModule
{
    public static void MapRoleEndpoints(this WebApplication app)
    {
        //getAllRoles
        app.MapGet("/role", getAllRoles)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns all roles.";
                operation.Parameters[0].Description = "Paging parameter. **limit** is the page size of resultset.";
                operation.Parameters[1].Description = "Paging parameter. **Token** is returned from last query.";
                operation.Parameters[2].Description = "RFC 5646 compliant language code.";
                return operation;
            })
         .Produces<DtoRole>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent);

        //getRole
        app.MapGet("/role/{roleId}", getRole)
            .WithOpenApi(operation =>
            {
                operation.Summary = "Returns requested role.";
                operation.Parameters[0].Description = "Id of the requested role.";
                operation.Parameters[1].Description = "RFC 5646 compliant language code.";
                return operation;
            })
            .Produces<DtoRole>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        //saveRole
        app.MapPost("/role", saveRole)
       .WithTopic("pubsub", "SaveRole")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Saves or updates requested role.";
                    return operation;
                })
                .Produces<DtoRole>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status201Created);

        //deleteRole
        app.MapDelete("/role/{roleId}", deleteRole)
                .WithOpenApi(operation =>
                {
                    operation.Summary = "Deletes existing role.";
                    return operation;
                })
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status204NoContent);
    }

    static IResponse<List<DtoRole>> getAllRoles(
        [FromServices] ResourceDBContext context,
        [FromQuery][Range(0, 100)] int page = 0,
        [FromQuery][Range(5, 100)] int pageSize = 100,
        [FromHeader(Name = "Language")] string? language = "en-EN"
        )
    {
        var roles = context!.Roles!
            .Include(t => t.Titles.Where(t => t.Language == language))
            .Skip(page * pageSize).Take(pageSize)
            .AsQueryable().ToList();

        if (roles.Count == 0)
        {
            return new Response<List<DtoRole>>
            {
                Data = null,
                Result = new amorphie.core.Base.Result(Status.Success, "Veri bulunamadı")
            };
        }

        return new Response<List<DtoRole>>
        {
            Data = roles.Select(x => ObjectMapper.Mapper.Map<DtoRole>(x)).ToList(),
            Result = new amorphie.core.Base.Result(Status.Success, "Getirme başarılı")
        };
    }

    static IResponse<DtoRole> getRole(
    [FromRoute(Name = "roleId")] Guid roleId,
    [FromServices] ResourceDBContext context,
    [FromHeader(Name = "Language")] string? language = "en-EN"
    )
    {
        var role = context!.Roles!
       .Include(t => t.Titles.Where(t => t.Language == language))
       .FirstOrDefault(t => t.Id == roleId);

        if (role == null)
        {
            return new Response<DtoRole>
            {
                Data = null,
                Result = new amorphie.core.Base.Result(Status.Success, "Veri bulunamadı")
            };
        }

        return new Response<DtoRole>
        {
            Data = ObjectMapper.Mapper.Map<DtoRole>(role),
            Result = new amorphie.core.Base.Result(Status.Success, "Getirme başarılı")
        };
    }

    static IResponse<DtoRole> saveRole(
        [FromBody] DtoSaveRoleRequest data,
        [FromServices] ResourceDBContext context
        )
    {
        Role? existingRecord = null;

        if (data.Id == null)
        {
            data.Id = Guid.NewGuid();
        }
        else
        {
            existingRecord = context?.Roles!.Include(t => t.Titles).FirstOrDefault(t => t.Id == data.Id);
        }

        if (existingRecord == null)
        {
            var role = ObjectMapper.Mapper.Map<Role>(data);

            role.CreatedAt = DateTime.UtcNow;
            context!.Roles!.Add(role);
            context.SaveChanges();

            return new Response<DtoRole>
            {
                Data = ObjectMapper.Mapper.Map<DtoRole>(role),
                Result = new amorphie.core.Base.Result(Status.Success, "Kaydedildi")
            };
        }
        else
        {
            if (CheckForUpdate(data, existingRecord!))
            {
                context!.Update(existingRecord);
                context!.SaveChanges();

                // try
                // {
                //     context!.Entry(existingRecord).State = EntityState.Modified;
                //     context!.SaveChanges();                    
                // }
                // catch (DbUpdateConcurrencyException ex)
                // {
                //     ex.Entries.Single().Reload(); context!.SaveChanges();
                // }

                return new Response<DtoRole>
                {
                    Data = ObjectMapper.Mapper.Map<DtoRole>(existingRecord),
                    Result = new amorphie.core.Base.Result(Status.Success, "Güncelleme Başarili")
                };
            }

            return new Response<DtoRole>
            {
                Data = ObjectMapper.Mapper.Map<DtoRole>(existingRecord),
                Result = new Result(Status.Error, "Değişiklik yok")
            };
        }
    }

    static bool CheckForUpdate(DtoSaveRoleRequest data, Role existingRecord)
    {
        var hasChanges = false;

        if (data.Status != null && data.Status != existingRecord.Status)
        {
            existingRecord.Status = data.Status;
            hasChanges = true;
        }

        if (data.Tags != null && data.Tags != existingRecord.Tags)
        {
            existingRecord.Tags = data.Tags;
            hasChanges = true;
        }
        var currentTime = DateTime.Now.ToUniversalTime();

        foreach (MultilanguageText multilanguageText in data.Titles)
        {
            var existingTitle = existingRecord.Titles.FirstOrDefault(t => t.Language == multilanguageText.Language);

            if (existingTitle == null)
            {
                // existingRecord.Titles.Add(ObjectMapper.Mapper.Map<Translation>(multilanguageText));
                //  context!.SaveChanges();
                // context!.Translations!.Add(ObjectMapper.Mapper.Map<Translation>(multilanguageText));

                Translation translation = new Translation();
                translation.CreatedBy = data.CreatedBy;
                translation.CreatedByBehalfOf = data.CreatedByBehalfOf;
                translation.Id = Guid.NewGuid();
                translation.Label = multilanguageText.Label;
                translation.Language = multilanguageText.Language;
                translation.ModifiedAt =  currentTime;
                translation.ModifiedByBehalfOf = data.ModifiedByBehalfOf;

                existingRecord.Titles.Add(translation);
                
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
            existingRecord.ModifiedAt = currentTime;
            return true;
        }
        else
        {
            return false;
        }
    }

    static IResponse deleteRole(
    [FromRoute(Name = "roleId")] Guid roleId,
    [FromServices] ResourceDBContext context)
    {
        var existingRecord = context?.Roles?.FirstOrDefault(t => t.Id == roleId);

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