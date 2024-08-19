using Elastic.Apm.Api;

public interface ICheckAuthorize
{
    ValueTask<IResult> Check(
                               CheckAuthorizeRequest request,
                               ResourceDBContext context,
                               HttpContext httpContext,
                               string headerClientId,
                               IConfiguration configuration,
                               ILogger logger,
                               CancellationToken cancellationToken
                        );
}
