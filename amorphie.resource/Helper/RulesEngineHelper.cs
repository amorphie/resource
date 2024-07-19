using System.Text;
using System.Text.Json;
using amorphie.core.Enums;
using amorphie.resource.Helper;
using Newtonsoft.Json;
using Serilog;

public static class Utils
{
    public static int ToInt(this object value)
    {
        try
        {
            return Convert.ToInt32(value);
        }
        catch
        {
            return 0;
        }
    }

    public static double ToDouble(this object value)
    {
        try
        {
            return Convert.ToDouble(value);
        }
        catch
        {
            return 0d;
        }
    }

    public static float ToFloat(this object value)
    {
        try
        {
            return (float)value;
        }
        catch
        {
            return 0f;
        }
    }

    public static bool ToBool(this object value)
    {
        try
        {
            return Convert.ToBoolean(value);
        }
        catch
        {
            return false;
        }
    }

    public static DateTime ToDateTime(this object value)
    {
        try
        {
            return Convert.ToDateTime(value);
        }
        catch
        {
            return DateTime.MinValue;
        }
    }

    public static string[] ToArray(this object value)
    {
        try
        {
            return JsonConvert.DeserializeObject<string[]>(value.ToString());
        }
        catch
        {
            return default;
        }
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
        return CallApi(url, null, HttpMethodType.GET, null);
    }

    public static CallApiResponse CallApiGet(string url, dynamic? header = null)
    {
        return CallApi(url, null, HttpMethodType.GET, header);
    }
    
    public static CallApiResponse CallApiPost(string url, dynamic body)
    {
        return CallApi(url, body, HttpMethodType.POST, null);
    }

    public static CallApiResponse CallApiPost(string url, dynamic body, dynamic? header = null)
    {
        return CallApi(url, body, HttpMethodType.POST, header);
    }

    private static CallApiResponse CallApi(string url, dynamic body, HttpMethodType httpMethodType,
        dynamic? header = null)
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

                using var httpContent =
                    new StringContent(Convert.ToString(jsonBody), Encoding.UTF8, "application/json");
                if (header != null)
                {
                    try
                    {
                        var headerJson = JsonConvert.SerializeObject(header);
                        Dictionary<string, object> headerDic =
                            JsonConvert.DeserializeObject<IDictionary<string, object>>(headerJson);
                        foreach (var item in headerDic)
                        {
                            if (CallApiConsts.IgnoreDefaultHeaders.Contains(item.Key) ||
                                CallApiConsts.ExcludeHeaders.Contains(item.Key.ToLower()))
                            {
                                continue;
                            }

                            if (!httpContent.Headers.Contains(item.Key))
                            {
                                httpContent.Headers.TryAddWithoutValidation(item.Key, item.Value.ToString());
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, "headers could not be processed");
                    }
                }

                response = await apiClient.PostAsync(url, httpContent);
            }
            else
            {
                using HttpRequestMessage request =
                    new HttpRequestMessage(HttpMethod.Get, url);
                if (header != null)
                {
                    try
                    {
                        var headerJson = JsonConvert.SerializeObject(header);
                        Dictionary<string, object> headerDic =
                            JsonConvert.DeserializeObject<IDictionary<string, object>>(headerJson);
                        foreach (var item in headerDic)
                        {
                            if (CallApiConsts.IgnoreDefaultHeaders.Contains(item.Key) ||
                                CallApiConsts.ExcludeHeaders.Contains(item.Key.ToLower()))
                            {
                                continue;
                            }

                            if (!request.Headers.Contains(item.Key))
                            {
                                request.Headers.TryAddWithoutValidation(item.Key, item.Value.ToString());
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, "headers could not be processed");
                    }
                }

                response = await apiClient.SendAsync(request);
            }

            // Check if the response is successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a JSON string
                string jsonContent = await response.Content.ReadAsStringAsync();
                ;

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
