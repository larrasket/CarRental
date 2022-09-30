using Microsoft.EntityFrameworkCore;
using Models.DataModels;
using Services;

namespace DataReporter;

public static class ReportBuilder
{
    private static readonly DataManager<Vehicle> Manager = new(null);

    public static void VehicleReport()
    {
        var data = Manager.Db.Set<Vehicle>().Include(x => x.Fines).ThenInclude(x => x.Bill).Include(x => x.Rents)
            .ThenInclude(x => x.Contract).Include(x => x.Maintenances).ThenInclude(x => x.Bill).AsEnumerable();
        var templateFile = new FileInfo("blank.xlsx");
        var outputFile = new FileInfo("out.xlsx ");
        var tmp = new List<Headings>() {new Headings()};
        var list = new List<VehicleReport>();
        foreach (Vehicle vehicle in data)
        {
            var report = new VehicleReport();
            report.CarNum = vehicle.Number;
            report.Regular = vehicle.Maintenances.Where(x => x.Type == TypeOfMaintenance.Regular)
                .Sum(x => x.Bill.Price);
            report.Cycle = vehicle.Maintenances.Where(x => x.Type == TypeOfMaintenance.Cycle)
                .Sum(x => x.Bill.Price);
            report.Fines = vehicle.Fines.Sum(x => x.Bill.Price);
            report.Total = vehicle.Rents.Sum(x => x.Contract.Price);
            list.Add(report);
        }

        if (File.Exists(outputFile.Name)) File.Delete(outputFile.Name);
        using var fastExcel = new FastExcel.FastExcel(templateFile, outputFile);
        fastExcel.Write(tmp, "Sheet1");
        // fastExcel.Write(list, "Sheet1");
    }
}

public class Headings
{
    // public const string StartDate = "موعد البداية";
    // public const string EndDate = "موعد النهاية";
    public const string CarNum = "رقم المركبة";
    public const string Regular = "مصاريف صيانة استثنائية";
    public const string Cycle = "مصاريف صيانة دورية";
    public const string Fines = "مصاريف غرامات";
    public const string Cost = "الدخل الكلي";
}