using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
public static class Utils
{
    public static bool CheckContains(string check, string valList)
    {
        if (String.IsNullOrEmpty(check) || String.IsNullOrEmpty(valList))
            return false;
 
        var list = valList.Split(',').ToList();
        return list.Contains(check);
    }
    public static bool CallApi(string url)
    {
        Console.WriteLine($"Call Api : {url}");
 
        return true;
    }
}
