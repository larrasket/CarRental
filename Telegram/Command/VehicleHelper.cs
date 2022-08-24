using System.Text;
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
        var v = await _vehicleManager.First(x => x.Number == selected, x => x.Rents);
        if (contracts) return (v, update)!;
        {
            while (v is null)
            {
                update = await _client.MessageWatcher(update);
                v = await _vehicleManager.First(x => x.Number == selected, x => x.Rents);
            }
        }

        return (v, update);
    }

    private static Task<string> ListBuilder(IEnumerable<Vehicle> vehicles, IEnumerable<Rent> rents, bool admin,
        bool allowContracts)
    {
        StringBuilder message = new();

        if (allowContracts == false)
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
            }
        else
            foreach (var rent in rents)
            {
                message.Append($"{Arabic.CarDetails.Number}: {rent.Vehicle.Number}");

                message.Append("\n");


                message.Append($"{Arabic.CarDetails.Model}: {rent.Vehicle.Model}");
                message.Append("\n");


                message.Append($"{Arabic.CarDetails.Brand}: {rent.Vehicle.Brand}");
                message.Append("\n");


                message.Append($"{Arabic.CarDetails.Color}: {rent.Vehicle.Color}");
                message.Append("\n");

                message.Append($"{Arabic.Rent.StartDay}: {rent.RentStart}");
                message.Append("\n");

                message.Append($"{Arabic.Rent.EndDay}: {rent.RentEnd}");
                message.Append("\n");
                message.Append("\n");
            }


        return Task.FromResult(message.ToString());
    }
}