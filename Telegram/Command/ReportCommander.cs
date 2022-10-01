using DataReporter;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;
using File = System.IO.File;

namespace Telegram.Command;

public partial class Commander
{
    public async Task Report(Update update)
    {
        var (vehicle, _) = await ChooseVehicle(update, admin: true);
        ReportBuilder.VehicleReport(vehicle.Number);
        Thread.Sleep(3000);
        var rs = Environment.GetEnvironmentVariable("HOME") + "/Merge/result.xlsx";
        var bts = await File.ReadAllBytesAsync(rs);
        var file = new InputFile(bts, $"report{vehicle.Number}.xlsx");
        await _client.SendDocumentAsync(update.ChatId(), file);
    }
}