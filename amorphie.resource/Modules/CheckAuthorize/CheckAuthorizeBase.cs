using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

public abstract class CheckAuthorizeBase
{
    public abstract ValueTask<IResult> Check(
                                    CheckAuthorizeRequest request,
                                    ResourceDBContext context,
                                    HttpContext httpContext,
                                    string headerClientId,
                                    IConfiguration configuration,
                                    CancellationToken cancellationToken
                             );


    protected async Task<Resource?> GetResource(
    ResourceDBContext context,
    CheckAuthorizeRequest request,
    CancellationToken cancellationToken
    )
    {
        return await context!.Resources!.AsNoTracking()
                                .FirstOrDefaultAsync(c => Regex.IsMatch(request.Url, c.Url), cancellationToken);
    }

    protected void RecursiveJsonLoop(JObject jsonObject, Dictionary<string, object> keyValuePairs, string currentPath)
    {
        foreach (var property in jsonObject.Properties())
        {
            string newPath = currentPath == "" ? property.Name : $"{currentPath}.{property.Name}";

            if (property.Value.Type == JTokenType.Object)
            {
                RecursiveJsonLoop((JObject)property.Value, keyValuePairs, newPath);
            }

            else if (property.Value.Type == JTokenType.Array)
            {
                for (int i = 0; i < ((JArray)property.Value).Count; i++)
                {
                    if (((JArray)property.Value)[i].Type == JTokenType.Object)
                        RecursiveJsonLoop((JObject)((JArray)property.Value)[i], keyValuePairs, $"{newPath}[{i}]");
                    else
                        keyValuePairs.Add($"{newPath}[{i}]", property.Value.ToString());
                }
            }
            else
            {
                keyValuePairs.Add($"{newPath}", property.Value.ToString());
            }
        }
    }
}