using amorphie.core.Module.minimal_api;

public class ResponseTransformationModule : BaseBBTRoute<DtoResponseTransformation, ResponseTransformation, ResourceDBContext>
{
    public ResponseTransformationModule(WebApplication app) : base(app)
    {
    }
    public override string[]? PropertyCheckList => new string[] { "ResponseCode" };

    public override string? UrlFragment => "responseTransformation";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);

        routeGroupBuilder.MapPost("transform", transform);
    }

    async ValueTask<IResult> transform(
        [FromBody] string responseCode,
        [FromServices] ResourceDBContext context,
        HttpContext httpContext,
        CancellationToken token
        )
    {
        var responseTransformation = await context!.ResponseTransformations!.AsNoTracking()
         .Include(t => t.ResponseTransformationMessages)
          .FirstOrDefaultAsync(t => t.ResponseCode == responseCode, token);

        if (responseTransformation == null)
        {
            return Results.Problem(detail: "Invalid Response Code", title: "Flow Exception", statusCode: 461);
        }

        return Results.Ok(ObjectMapper.Mapper.Map<DtoGetResponseTransformation>(responseTransformation));
    }
}