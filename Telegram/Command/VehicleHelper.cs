using Models.DataModels;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Languages;

namespace Telegram.Command;

public partial class Commander
{
    private async Task<(Vehicle v, Update update)> ChooseVehicle(Update update, bool admin = false,
        bool contracts = false)
    {
        await ListVehicles(update, admin, contracts);
        await _client.SendMessageAsync(update.ChatId(), Arabic.CarDetails.EnterNumber);
        update = await _client.MessageWatcher(update);

        var selected = update.Text()[1..];
        var v = _vehicleManager.All().FirstOrDefault(x => x.Number == selected);
        if (contracts) return (v, update);
        {
            while (v is null)
            {
                update = await _client.MessageWatcher(update);
                v = _vehicleManager.All().FirstOrDefault(x => x.Number == selected);
            }
        }

        return (v, update);
    }
}