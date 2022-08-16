// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json.Linq;

class program
{
    void testsomthing()
    {
        var name = "ConnectionString";
        string rs;
        var ta = File.ReadAllText("LocalData.json");
        var s = JObject.Parse(ta);
        var ii = s["connectionString"];
        Console.WriteLine(ii);
    }

    private static void Main()
    {
        testparse();
        DateOnly x = new DateOnly(22, 1, 3);
        DateOnly y = new DateOnly(22, 1, 1);
        Console.WriteLine(y < x);
    }

    static void testparse()
    {
        var x = "1/1/2002";
        var s = "1/5";
        DateTime f;

        var d1 = DateTime.TryParse(s, out f);
        Console.WriteLine(f);
    }
}