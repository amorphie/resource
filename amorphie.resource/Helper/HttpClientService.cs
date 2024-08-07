using System.Text;
using Elastic.Apm.Api;
using Newtonsoft.Json;

namespace amorphie.resource.Helper;

public class HttpClientService
{
    private readonly HttpClient _client;
    private readonly HashSet<string> _bypassHeaders;
    private readonly HashSet<string> _contentHeaders;

    public HttpClientService()
    {
        _client = new HttpClient();
        _bypassHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Allow", "Content-Disposition", "Content-Encoding", "Content-Language",
            "Content-Length", "Content-Location", "Content-MD5", "Content-Range",
            "Content-Type", "Expires", "Last-Modified", "Transfer-Encoding",
            "Referer", "User-Agent", "Connection", "Host", "Cache-Control", "Accept-Encoding", "Authorization"
        };

        _contentHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Allow", "Content-Disposition", "Content-Encoding", "Content-Language",
            "Content-Length", "Content-Location", "Content-MD5", "Content-Range",
            "Content-Type", "Expires", "Last-Modified"
        };
    }

    public async Task<HttpResponseMessage> SendRequestAsync(
        string url,
        dynamic data,
        string method,
        dynamic headers,
        ISpan span)
    {
        span.SetLabel("CallApi.Url", url);
        span.SetLabel("CallApi.Method", method);
        // Create HttpRequestMessage
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri(url),
            Method = new HttpMethod(method.ToUpper())
        };

        // Add Headers to the request
        AddHeaders(request, headers, span);

        // If method is POST and data is provided, add the content
        if (method.ToUpper() == "POST" && data != null)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            span.SetLabel("CallApi.RequestBody", jsonData);
        }

        // Send the request
        return await _client.SendAsync(request);
    }

    private void AddHeaders(HttpRequestMessage request, dynamic headers, ISpan span)
    {
        if (headers == null)
        {
            span.SetLabel("CallApi.Header", "none");
            return;
        }

        Dictionary<string, object> headerDic;
        try
        {
            var headerJson = JsonConvert.SerializeObject(headers);
            headerDic =
                JsonConvert.DeserializeObject<IDictionary<string, object>>(headerJson);
        }
        catch (Exception ex)
        {
            span.CaptureErrorLog(new ErrorLog($"An error occurred during header serialization. Error: {ex.Message}"), null, ex);
            return;
        }

        foreach (var header in headerDic)
        {
            try
            {
                if (_bypassHeaders.Contains(header.Key))
                {
                    continue;
                }

                if (_contentHeaders.Contains(header.Key))
                {
                    if (request.Content == null)
                    {
                        request.Content = new StringContent(string.Empty);
                    }

                    request.Content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToString().ToAscii());
                }
                else
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToString().ToAscii());
                }
            }
            catch (Exception ex)
            {
                span.CaptureErrorLog(new ErrorLog($"Failed to add header: {header.Key}. Error: {ex.Message}"), null, ex);
            }
        }
    }
}
