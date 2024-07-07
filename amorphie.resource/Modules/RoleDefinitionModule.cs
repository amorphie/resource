using amorphie.core.Module.minimal_api;

public class RoleDefinitionModule : BaseBBTRoute<DtoRoleDefinition, RoleDefinition, ResourceDBContext>
{
    public RoleDefinitionModule(WebApplication app) : base(app)
    {
    }

    public override string[]? PropertyCheckList => new string[] { };

    public override string? UrlFragment => "roleDefinition";

    protected async ValueTask<IResult> UpsertRoleDefinition(
      [FromServices] ResourceDBContext context,
      [FromBody] IEnumerable<DtoRoleDefinition> dtoRoleDefinitions,
      HttpContext httpContext,
      CancellationToken token
      )
    {
        foreach(var dtoRoleDefinition in dtoRoleDefinitions)
        {
            var roleDefinition = await context.RoleDefinitions!.FirstOrDefaultAsync(r => r.Id.Equals(dtoRoleDefinition.Id));
            if(roleDefinition is {})
            {
                roleDefinition.Key = dtoRoleDefinition.Key;
                roleDefinition.Description = dtoRoleDefinition.Description;
                roleDefinition.Status = dtoRoleDefinition.Status;
            }
            else
            {
                await context.RoleDefinitions!.AddAsync(new RoleDefinition{
                    Key = dtoRoleDefinition.Key,
                    Description = dtoRoleDefinition.Description,
                    Status = dtoRoleDefinition.Status,
                    Id = dtoRoleDefinition.Id,
                    Tags = dtoRoleDefinition.Tags,
                    CreatedAt = DateTime.Now,
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    CreatedByBehalfOf = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    ModifiedByBehalfOf = Guid.Parse("00000000-0000-0000-0000-000000000000")
                });
            }
        }
        await context.SaveChangesAsync();
        return Results.Ok();
    }

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapPost("upsert", UpsertRoleDefinition);
    }
}