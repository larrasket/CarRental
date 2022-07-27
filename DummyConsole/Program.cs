// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json.Linq;

var name = "ConnectionString";
string rs;
var ta = File.ReadAllText("LocalData.json");
var s = JObject.Parse(ta);
var ii = s["connectionString"];
Console.WriteLine(ii);