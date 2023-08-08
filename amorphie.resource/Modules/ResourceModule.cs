using amorphie.resource;
using amorphie.resource.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
public class ResourceModule : BaseResourceModule<DtoResource, Resource, ResourceValidator>
{
    public ResourceModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Type", "Url", "Status" };

    public override string? UrlFragment => "resource";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapPost("workflowStatus", saveResourceWithWorkflow);
    }
    public async   ValueTask<IResult> saveResourceWithWorkflow(
        [FromBody] DtoPostWorkflow data,
        [FromServices] ResourceDBContext context,
        CancellationToken cancellationToken
        )
    {

        var existingRecord = await context!.Resources!.Include(i=>i.DisplayNames)
        .Include(i=>i.Descriptions).FirstOrDefaultAsync(t => t.Id == data.recordId);


        if (existingRecord == null)
        {
            var resource = ObjectMapper.Mapper.Map<Resource>(data.entityData!);
            resource.Id= data.recordId;
            context!.Resources!.Add(resource);
            await context.SaveChangesAsync(cancellationToken);
            return Results.Ok(resource);
            
        }
        else
        {
            //Apply update to only changed fields.
            if (SaveResourceUpdate(data.entityData!, existingRecord,context))
            {
               await context!.SaveChangesAsync(cancellationToken);
                

            }
            return Results.Ok();

           
        }
    }
    private static bool SaveResourceUpdate(DtoResource data, Resource existingRecord,ResourceDBContext context)
    {
        var hasChanges = false;
        // Apply update to only changed fields.
        if (data.Url != null && data.Url != existingRecord.Url)
        {
            existingRecord.Url = data.Url;
            hasChanges = true;
        }
        if (data.Type != null && data.Type != existingRecord.Type)
        {
            existingRecord.Type = data.Type;
            hasChanges = true;
        }
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
          if (data.DisplayNames!=null&&data.DisplayNames.Count>0)
        {
            foreach (var name in data.DisplayNames)
            {
                amorphie.core.Base.Translation? translation =  existingRecord.DisplayNames.FirstOrDefault(f=>f.Language==name.Language);
                if (translation!=null&&translation.Label!=name.Label)
                {
                    translation.Label=name.Label;
                     hasChanges = true;
                }
                if(translation==null)
                {
                    existingRecord.DisplayNames.Add(new amorphie.core.Base.Translation(){
                        Label=name.Label,
                        Language=name.Language
                    });
                     hasChanges = true;
                }
            }
        }
        if (data.Descriptions!=null&&data.Descriptions.Count>0)
        {
            foreach (var name in data.Descriptions)
            {
                amorphie.core.Base.Translation? translation =  existingRecord.Descriptions.FirstOrDefault(f=>f.Language==name.Language);
                if (translation!=null&&translation.Label!=name.Label)
                {
                    translation.Label=name.Label;
                     hasChanges = true;
                }
                if(translation==null)
                {
                    existingRecord.Descriptions.Add(new amorphie.core.Base.Translation(){
                        Label=name.Label,
                        Language=name.Language
                    });
                     hasChanges = true;
                }
            }
        }
        return hasChanges;
    }

  
}
