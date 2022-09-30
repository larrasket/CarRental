using System.Linq.Expressions;
using System.Text;
using Models.DataModels;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Languages;

namespace Telegram.Command;

public partial class Commander
{
    private async Task<(Vehicle v, Update update)> ChooseVehicle(Update update, bool admin = false,
        bool contracts = false, bool rents = false, params Expression<Func<Vehicle, Object>>[]? includeExp)
    {
        await ListVehicles(update, admin, contracts, rents);
        await _client.SendMessageAsync(update.ChatId(), Arabic.EnterNumber);
        update = await _client.MessageWatcher(update);

        var selected = update.Text()[1..];
        var v = await _vehicleManager.First(x => x.Number == selected, includeExp);
        if (contracts || rents) return (v, update)!;
        {
            while (v is null)
            {
                update = await _client.MessageWatcher(update);
                v = await _vehicleManager.First(x => x.Number == selected, includeExp);
            }
        }

        return (v, update);
    }

    private static Task<string> ListBuilder(IEnumerable<Vehicle> vehicles, bool admin,
        bool allowContracts, bool allowRents)
    {
        StringBuilder message = new();
        foreach (var vehicle in vehicles)
        {
            if (admin) message.Append($"{Arabic.CarDetails.Number}: /{vehicle.Number}");
            else message.Append($"{Arabic.CarDetails.Number}: {vehicle.Number}");

            message.Append("\n");
            message.Append($"{Arabic.CarDetails.Model}: {vehicle.Model}");
            message.Append("\n");
            message.Append($"{Arabic.CarDetails.Brand}: {vehicle.Brand}");
            message.Append("\n");
            message.Append($"{Arabic.CarDetails.Color}: {vehicle.Color}");
            message.Append("\n");
            message.Append("\n");
            if (!allowContracts && !allowRents) continue;
            var n = DateOnly.FromDateTime(DateTime.Today);
            var current =
                vehicle.Rents.Where(x => x.Status != Status.Cancelled && x.RentStart >= n || x.RentEnd >= n);
            foreach (var rent in current)
            {
                message.Append($"{Arabic.Rent.StartDay}: {rent.RentStart.Date()}");
                message.Append("\n");
                message.Append($"{Arabic.Rent.EndDay}: {rent.RentEnd.Date()}");
                message.Append("\n");
                if (allowContracts)
                    message.Append($"{Arabic.CarDetails.Contract}: /cont{rent.BillId}");
                else
                    message.Append($"{Arabic.CarDetails.Rent}: /rent{rent.Id}");
                message.Append("\n");
            }

            message.Append("\n");
        }

        return Task.FromResult(message.ToString());
    }
}