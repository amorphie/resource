using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class Utils
{
    public static bool CheckContains(string check, string valList)
    {
        if (String.IsNullOrEmpty(check) || String.IsNullOrEmpty(valList))
            return false;

        var list = valList.Split(',').ToList();
        return list.Contains(check);
    }

    public static bool CallGetApi(Dictionary<string, string> parameters, string url)
    {
        foreach (var variable in parameters)
            url = url.Replace(variable.Key, variable.Value);

        var apiClient = new HttpClient();

        var response = apiClient.GetAsync(url).Result;

        return response.IsSuccessStatusCode;
    }

    public static bool CallPostApi(string data, Dictionary<string, string> parameters, string url)
    {
        foreach (var variable in parameters)
            url = url.Replace(variable.Key, variable.Value);

        var apiClient = new HttpClient();

        var postData = string.IsNullOrEmpty(data) ? "" : data;
        var httpContent = new StringContent(postData, Encoding.UTF8, "application/json");

        var response = apiClient.PostAsync(url, httpContent).Result;

        return response.IsSuccessStatusCode;
    }

    public static string GetHeader(IHeaderDictionary headers, string name)
    {
        return headers[name];
    }

    public static string GetPath(string[] paths, string name)
    {
        var match = Regex.Match(paths[0], paths[1]);

        if (match.Success)
        {
            foreach (Group pathVariable in match.Groups)
            {
                if (pathVariable.Name == name)
                    return pathVariable.Value;
            }
        }

        return "";
    }

    public static string GetBody(string body, string path)
    {
        JObject jsonObject = JsonConvert.DeserializeObject<JObject>(body);

        return Helper.GetJsonValue(jsonObject, path);
    }
}
