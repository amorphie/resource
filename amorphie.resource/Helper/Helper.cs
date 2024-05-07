using Newtonsoft.Json.Linq;

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

    public static string GetJsonValue(JObject obj, string path)
    {
        var retVal = "";

        var token = obj.SelectToken(path);

        if (token != null)
        {
            retVal = token.ToString();
        }

        return retVal;
    }
}