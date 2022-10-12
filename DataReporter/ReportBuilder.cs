using System.Diagnostics;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;
using Services;

namespace DataReporter;

public static class ReportBuilder
{
    private static readonly DataManager<Vehicle> Manager = new(null);

    private static readonly FileInfo TemplateFile = new("blank.xlsx");

    private static readonly FineReport HeadFineReport = new()
    {
        Id = "معرف الغرامة",
        Date = "تاريخ الغرامة",
        Total = "تكلفة الغرامة"
    };


    private static readonly RentReport HeadRentReport = new()
    {
        StartDay = "موعد البداية",
        EndDay = "موعد النهاية",
        Total = "الدخل الكلي",
        Id = "معرف الإيجار",
        ContractId = "معرف عقد الإيجار",
    };

    private static readonly MaintenanceReport HeadMaintenanceReport = new()
    {
        Id = "معرف الصيانة",
        Type = "نوع الصيانة",
        Date = "تاريخ الصيانة",
        Total = "تكلفة الصيانة",
    };

    public static void VehicleReport(string v)
    {
        Build(v);
        var home = Environment.GetEnvironmentVariable("HOME");
        var startInfo = new ProcessStartInfo()
            {FileName = "/usr/bin/python3", Arguments = home + "/Merge/script.py",};
        var proc = new Process {StartInfo = startInfo,};
        proc.Start();
        proc.WaitForExit();
    }

    private static void Build(string v)
    {
        var home = Environment.GetEnvironmentVariable("HOME");
        string f1 = home + "/Merge/files/rents.xlsx";
        string f2 = home + "/Merge/files/fines.xlsx";
        string f3 = home + "/Merge/files/maintenances.xlsx";
        string f5 = home + "/Merge/result.xlsx";
        if (File.Exists(f1)) File.Delete(f1);
        if (File.Exists(f2)) File.Delete(f2);
        if (File.Exists(f3)) File.Delete(f3);
        if (File.Exists(f5)) File.Delete(f5);
        var data = Manager.Db.Set<Vehicle>().Where(x => x.Number == v)
            .Include(x => x.Fines)
            .ThenInclude(x => x.Bill)
            .Include(x => x.Fines)
            .ThenInclude(x => x.Creation)
            
            
            
            .Include(x => x.Rents)
            .ThenInclude(x => x.Contract).Include(x => x.Maintenances)
            .ThenInclude(x => x.Bill)
            
            
            
            .Include(x => x.Rents)
            .ThenInclude(x => x.Contract).Include(x => x.Maintenances)
            .ThenInclude(x => x.Creation)
            
            .First();
        var rentReports = new List<RentReport> {HeadRentReport};
        rentReports.AddRange(data.Rents.Where(x => x.Status != Status.Cancelled).Select(rent => new RentReport
        {
            StartDay = rent.RentStart.ToString(),
            EndDay = rent.RentEnd.ToString(),
            Total = rent.Contract.Price.ToString(CultureInfo.InvariantCulture),
            Id = rent.Id.ToString(),
            ContractId = rent.Contract.Id.ToString()
        }));
        List<FineReport> fineReports = new() {HeadFineReport};
        fineReports.AddRange(data.Fines.Select(fine => new FineReport
        {
            Date = fine.Creation?.CreatedDateTime.ToShortDateString(), Id = fine.Bill.Id.ToString(),
            Total = fine.Bill.Price.ToString(CultureInfo.InvariantCulture)
        }));
        List<MaintenanceReport> maintenanceReports = new() {HeadMaintenanceReport};
        maintenanceReports.AddRange(data.Maintenances.Select(maintenance => new MaintenanceReport
        {
            Date = maintenance.Creation?.CreatedDateTime.ToShortDateString(),
            Id = maintenance.Bill.Id.ToString(),
            Total = maintenance.Bill.Price.ToString(CultureInfo.InvariantCulture),
            Type = maintenance.Type == TypeOfMaintenance.Cycle ? "صيانة دورية" : "صيانة استثنائية"
        }));
        using var fastExcel1 = new FastExcel.FastExcel(TemplateFile, new FileInfo(f1));
        fastExcel1.Write(rentReports, "Sheet1");
        using var fastExcel2 = new FastExcel.FastExcel(TemplateFile, new FileInfo(f2));
        fastExcel2.Write(fineReports, "Sheet1");
        using var fastExcel3 = new FastExcel.FastExcel(TemplateFile, new FileInfo(f3));
        fastExcel3.Write(maintenanceReports, "Sheet1");
    }
}