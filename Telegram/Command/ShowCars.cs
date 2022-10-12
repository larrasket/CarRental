using Models.DataModels;
using Models.Helpers;
using Telegram.BotAPI.AvailableMethods;
using Telegram.Languages;
using Update = Telegram.BotAPI.GettingUpdates.Update;

namespace Telegram.Command;

public partial class Commander
{
    public async Task ShowCars(Status status, Update update)
    {
        switch (status)
        {
            case Status.Completed:
                await ShowCarsInCurrentRent(false, update);
                break;
            case Status.Waiting:
                await ShowCarsInCurrentRent(true, update);
                break;
            case Status.Cancelled:
            default:
                await TomorrowReceive(update);
                await NotReceived(update);
                await ShowCarsInCurrentRent(true, update);
                await ShowCarsInCurrentRent(false, update);
                break;
        }
    }

    private async Task NotReceived(Update update)
    {
        var rents = _rentManager.Where(
            x => x.RentStart < DateOnly.FromDateTime(DateTime.Today) && x.Status == Status.Waiting, x => x.Vehicle);
        var enumerable = rents as Rent[] ?? rents.ToArray();
        if (!enumerable.Any()) return;
        await _client.SendMessageAsync(update.ChatId(), Arabic.NotReceived);
        var message = await ListBuilder(enumerable.Select(x => x.Vehicle), false, false, false);
        await _client.SendMessageAsync(update.ChatId(), message);
    }


    private async Task TomorrowReceive(Update update)
    {
        var tomorrow = DateOnly.FromDateTime(DateTime.Today).AddDays(1);
        var rents = _rentManager.Where(x => x.RentEnd == tomorrow && x.Status == Status.Waiting);
        var enumerable = rents as Rent[] ?? rents.ToArray();
        if (!enumerable.Any()) return;
        await _client.SendMessageAsync(update.ChatId(), Arabic.TomorrowReceive);
        var message = await ListBuilder(enumerable.Select(x => x.Vehicle), false, false, false);
        await _client.SendMessageAsync(update.ChatId(), message);
    }

    private async Task ShowCarsInCurrentRent(bool current, Update update)
    {
        var vehicles = current
            ? _vehicleManager.Where(x => x.InRent(), x => x.Rents)
            : _vehicleManager.Where(x => !x.InRent(), x => x.Rents);

        var message = await ListBuilder(vehicles, false, false, current);
        if (!string.IsNullOrEmpty(message))
            await _client.SendMessageAsync(update.ChatId(), message);
    }
}