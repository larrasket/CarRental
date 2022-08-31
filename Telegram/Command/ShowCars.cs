using Models.DataModels;
using Models.Helpers;
using Telegram.BotAPI.AvailableMethods;
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
                await ShowCarsInCurrentRent(true, update);
                await ShowCarsInCurrentRent(false, update);
                break;
        }
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