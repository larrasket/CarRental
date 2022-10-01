// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
using System.Reflection;
using DataReporter;
using Newtonsoft.Json.Linq;

namespace DummyConsole;

class Program
{
    public class User
    {
        public int Id { get; set; }
        public int Reputation { get; set; }
        public string DisplayName { get; set; }
        public DateTime LastAccessDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string WebSiteUrl { get; set; }
        public int Views { get; set; }
        public int Age { get; set; }
        public int UpVotes { get; set; }
        public int downVotes { get; set; }
        public string Location { get; set; }
        public string AboutMe { get; set; }
    };

    void testsomthing()
    {
        var name = "ConnectionString";
        string rs;
        var ta = File.ReadAllText("LocalData.json");
        var s = JObject.Parse(ta);
        var ii = s["connectionString"];
        Console.WriteLine(ii);
    }


    private static void what()
    {
        var user = new User()
        {
            Id = 1,
            Reputation = 2,
            DisplayName = "fucker",
            UpVotes = 31,
        };

        foreach (PropertyInfo prop in typeof(User).GetProperties())
        {
            Console.WriteLine("{0} = {1}", prop.Name, prop.GetValue(user, null));
        }
    }

    private static void test()
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

    static void ff()
    {
        var templateFile = new FileInfo("/home/ghd/qc.xlsx");
        var outputFile = new FileInfo("/home/ghd/out.xlsx ");
        using var fastExcel = new FastExcel.FastExcel(templateFile, outputFile);
        var objectList = new List<MyObject>();
        for (var rowNumber = 1; rowNumber < 100000; rowNumber++)
        {
            var genericObject = new MyObject();
            genericObject.StringColumn1 = "A string " + rowNumber.ToString();
            genericObject.IntegerColumn2 = 45678854;
            genericObject.DoubleColumn3 = 87.01d;
            genericObject.ObjectColumn4 = DateTime.Now.ToLongTimeString();
            objectList.Add(genericObject);
        }

        fastExcel.Write(objectList, "sheet1", true);
    }

    static void Main()
    {
        ReportBuilder.VehicleReport("41414");
    }
    
}


public class MyObject
{
    [DisplayName("something")] public string StringColumn1 { get; set; }

    [DisplayName("something")] public int IntegerColumn2 { get; set; }

    [DisplayName("something")] public double DoubleColumn3 { get; set; }

    [DisplayName("something")] public string ObjectColumn4 { get; set; }
}