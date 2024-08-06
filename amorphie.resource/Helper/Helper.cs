using amorphie.resource.core.Enum;

public static class Helper
{
    public static string GetHeaderLanguage(this HttpContext httpContext)
    {
        var language = "en-EN";

        if (httpContext.Request.Headers.ContainsKey("Language"))
        {
            language = httpContext.Request.Headers["Language"].ToString();
        }

        return language;
    }

    public static ResourceType ToResourceType(this string value)
    {
        return value.ToLower() switch
        {
            "connect" => ResourceType.CONNECT,
            "delete" => ResourceType.DELETE,
            "get" => ResourceType.GET,
            "head" => ResourceType.HEAD,
            "options" => ResourceType.OPTIONS,
            "post" => ResourceType.POST,
            "put" => ResourceType.PUT,
            "trace" => ResourceType.TRACE,
            _ => ResourceType.ALL
        };
    }
}
