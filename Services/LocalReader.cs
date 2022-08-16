using Newtonsoft.Json.Linq;

namespace Services;

public static class LocalReader
{
    public static string GetObj(string name)
    {
        var s = JObject.Parse(File.ReadAllText("LocalData.json"));
        return s[name].ToString();
    }

    public static List<string> GetList(string name) => throw new NotImplementedException();
}