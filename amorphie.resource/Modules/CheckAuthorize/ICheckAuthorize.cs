public interface ICheckAuthorize
{
    ValueTask<IResult> Check(
                               CheckAuthorizeRequest request,
                               ResourceDBContext context,
                               HttpContext httpContext,
                               string headerClientId,
                               IConfiguration configuration,
                               CancellationToken cancellationToken
                        );
}