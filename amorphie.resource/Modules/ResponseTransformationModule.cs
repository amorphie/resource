using amorphie.core.Module.minimal_api;
using Newtonsoft.Json.Linq;

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
        [FromBody] DtoGetResponseTransformationRequest request,
        [FromServices] ResourceDBContext context,
        HttpContext httpContext,
         [FromHeader(Name = "clientId")] string headerClientId,
         [FromHeader(Name = "Accept-Language")] string headerAcceptLanguage,
        CancellationToken token
        )
    {
        var responseTransformationList = await context!.ResponseTransformations!.AsNoTracking()
         .Include(t => t.ResponseTransformationMessages.Where(t => t.Language == headerAcceptLanguage))
          .Where(t => t.ResponseCode == request.ResponseCode && t.Audience!.Contains(headerClientId)
                                ).ToListAsync(token);

        if (responseTransformationList != null && responseTransformationList.Count > 0)
        {
            foreach (ResponseTransformation responseTransformation in responseTransformationList)
            {
                var filter = responseTransformation.Filter;

                if (filter != null && filter.Contains("=="))
                {
                    var tmp = filter.Split("==");

                    var filterJsonPath = tmp[0];
                    var filterValue = tmp[1];

                    JObject o = JObject.Parse(request.Body!);

                    JToken? bodyFilteredValue = o.SelectToken(filterJsonPath);

                    if (bodyFilteredValue != null && bodyFilteredValue.ToObject<string>()!.Trim() == filterValue.Trim())
                    {
                        return Results.Ok(ObjectMapper.Mapper.Map<DtoGetResponseTransformation>(responseTransformation));
                    }
                }

            }
        }
        return Results.Problem(detail: "Invalid Response", title: "Flow Exception", statusCode: 461);
    }
}