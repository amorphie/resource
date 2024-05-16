using System.Text;
using System.Text.Json;
using amorphie.core.Enums;
using Newtonsoft.Json;
public static class Utils
{
    public static int ToInt(this object value)
    {
        try { return Convert.ToInt32(value); }
        catch { return 0; }
    }
    public static double ToDouble(this object value)
    {
        try { return Convert.ToDouble(value); }
        catch { return 0d; }
    }
    public static float ToFloat(this object value)
    {
        try { return (float)value; }
        catch { return 0f; }
    }
    public static bool ToBool(this object value)
    {
        try { return Convert.ToBoolean(value); }
        catch { return false; }
    }
    public static DateTime ToDateTime(this object value)
    {
        try { return Convert.ToDateTime(value); }
        catch { return DateTime.MinValue; }
    }

    public static string[] ToArray(this object value)
    {
        try { return JsonConvert.DeserializeObject<string[]>(value.ToString()); }
        catch { return default; }
    }

    public static bool CheckContains(string check, string valList)
    {
        if (string.IsNullOrEmpty(check) || string.IsNullOrEmpty(valList))
            return false;

        var list = valList.Split(',').ToList();
        return list.Contains(check);
    }

    public static CallApiResponse CallApiGet(string url)
    {
        return CallApi(url, null, HttpMethodType.GET);
    }

    public static CallApiResponse CallApiPost(string url, dynamic body)
    {
        return CallApi(url, body, HttpMethodType.POST);
    }
    private static CallApiResponse CallApi(string url, dynamic body, HttpMethodType httpMethodType)
    {
        var apiClient = new HttpClient();

        var response = new HttpResponseMessage();
        dynamic data = null;

        Task.Run(async () =>
        {
            if (httpMethodType == HttpMethodType.POST)
            {
                if (body == null)
                    body = "";

                var jsonBody = JsonConvert.SerializeObject(body);

                var httpContent = new StringContent(Convert.ToString(jsonBody), Encoding.UTF8, "application/json");
                response = await apiClient.PostAsync(url, httpContent);
            }
            else
            {
                response = await apiClient.GetAsync(url);
            }

            // Check if the response is successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a JSON string
                string jsonContent = await response.Content.ReadAsStringAsync(); ;

                using (JsonDocument jsonDocument = JsonDocument.Parse(jsonContent))
                {
                    // Convert the JsonDocument to a dynamic object
                    data = DeserializeJsonDocument(jsonDocument);
                }
            }

        }).Wait();

        var result = new CallApiResponse() { IsSuccessStatusCode = response.IsSuccessStatusCode, Data = data };

        return result;
    }

    static dynamic DeserializeJsonDocument(JsonDocument jsonDocument)
    {
        // Create an empty dynamic object
        dynamic dynamicObject = new System.Dynamic.ExpandoObject();

        // Iterate over each property in the JsonDocument
        foreach (JsonProperty property in jsonDocument.RootElement.EnumerateObject())
        {
            // Add the property to the dynamic object
            ((IDictionary<String, Object>)dynamicObject)[property.Name] = GetValue(property.Value);
        }

        return dynamicObject;
    }

    static object GetValue(JsonElement jsonElement)
    {
        // Handle different value types
        switch (jsonElement.ValueKind)
        {
            case JsonValueKind.Number:
                return jsonElement.GetDouble(); // Or GetInt32(), GetInt64(), etc. depending on your needs
            case JsonValueKind.String:
                return jsonElement.GetString();
            case JsonValueKind.True:
                return true;
            case JsonValueKind.False:
                return false;
            case JsonValueKind.Null:
                return null;
            default:
                return jsonElement.ToString();
        }
    }
}

public class CallApiResponse
{
    public bool IsSuccessStatusCode;
    public dynamic Data;
}