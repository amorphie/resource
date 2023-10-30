using amorphie.core.Module.minimal_api;
using amorphie.resource;

public class PrivilegeModule : BaseBBTRoute<DtoPrivilege, Privilege, ResourceDBContext>
{
    public PrivilegeModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { "Ttl", "Url", "Status" };

    public override string? UrlFragment => "privilege";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);

        routeGroupBuilder.MapPost("workflowStatus", saveWithWorkflow);
    }

    public async ValueTask<IResult> saveWithWorkflow(
      [FromBody] DtoPrivilegeWorkflow data,
      [FromServices] ResourceDBContext context,
      CancellationToken cancellationToken
      )
    {
        var existingRecord = await context!.Privileges!.FirstOrDefaultAsync(t => t.Id == data.recordId);

        if (existingRecord == null)
        {
            var privilege = ObjectMapper.Mapper.Map<Privilege>(data.entityData!);
            privilege.Id = data.recordId;
            context!.Privileges!.Add(privilege);
            await context.SaveChangesAsync(cancellationToken);
            return Results.Ok(privilege);
        }
        else
        {
            if (SavePrivilegeUpdate(data.entityData!, existingRecord, context))
            {
                await context!.SaveChangesAsync(cancellationToken);
            }

            return Results.Ok();
        }
    }

    private static bool SavePrivilegeUpdate(DtoPrivilege data, Privilege existingRecord, ResourceDBContext context)
    {
        var hasChanges = false;

        if (data.Url != null && data.Url != existingRecord.Url)
        {
            existingRecord.Url = data.Url;
            hasChanges = true;
        }

        return hasChanges;
    }
}