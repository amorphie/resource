using System.Text;
public static class Utils
{
    public static bool CheckContains(string check, string valList)
    {
        if (string.IsNullOrEmpty(check) || string.IsNullOrEmpty(valList))
            return false;

        var list = valList.Split(',').ToList();
        return list.Contains(check);
    }

    public static HttpResponseMessage CallApi(string url)
    {
        var apiClient = new HttpClient();

        var response = new HttpResponseMessage();

        Task.Run(async () =>
        {
            response = await apiClient.GetAsync(url);
        }).Wait();

        return response;
    }
    public static HttpResponseMessage CallApi(string url, dynamic body)
    {
        var apiClient = new HttpClient();
        var httpContent = new StringContent(Convert.ToString(body), Encoding.UTF8, "application/json");

        var response = new HttpResponseMessage();

        Task.Run(async () =>
        {
            response = await apiClient.PostAsync(url, httpContent);
        }).Wait();

        return response;
    }
}
