namespace amorphie.resource.Helper;

public static class CallApiConsts
{
    public static string[] IgnoreDefaultHeaders =
    {
        "Host", "Accept", "Connection", "User-Agent", "Accept-Encoding", "Content-Type", "Content-Length",
        "Cache-Control", "Content-Encoding", "Content-Range", "Referer", "Range", "Transfer-Encoding"
    };

    public static string[] ExcludeHeaders = { "authorization" };
}
