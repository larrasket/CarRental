namespace Telegram.Command;

using System.Text;
using Models.DataModels;
using Models.Helpers;
using Services;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Languages;

public partial class Commander
{
    private readonly TypeManager<Vehicle> _vehicleManager = new TypeManager<Vehicle>();

    public async Task<Update> AddVehicle(Update update)
    {
        var vehicle = new Vehicle();

        update = await GenericOptions(update, _vehicleManager.All().Select(x => x.Brand), Arabic.AddBrand);
        vehicle.Brand = update.Text();

        update = await GenericOptions(update, _vehicleManager.All().Select(x => x.Model), Arabic.AddModel);
        vehicle.Model = update.Text();

        update = await GenericOptions(update, _vehicleManager.All().Select(x => x.Color), Arabic.AddColor);
        vehicle.Color = update.Text();

        var numbers = _vehicleManager.All().Select(x => x.Number);
        while (true)
        {
            update = await GenericOptions(update, _vehicleManager.All().Select(x => x.Number), Arabic.AddNumber);
            if (numbers.Any(x => x == update.Text()) && update.Text() != "/cancel")
            {
                await _client.SendMessageAsync(update.ChatId(), Arabic.CarNumberAlreadyExist);
            }
            else if (update.Text() == "/cancel")
            {
                return update;
            }
            else break;
        }

        vehicle.Number = update.Text();
        vehicle.CreatedBy = update.UserName();
        var rs = await _vehicleManager.Add(vehicle);
        if (rs != 1) throw new Exception();
        return update;
    }


    public Task ListVehicles(Update? update, bool admin = false, bool allowContracts = false)
    {
        var vehicles = _vehicleManager.All();
        var rents = _rentManager.All();
        var message = new StringBuilder();
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

            var now = DateOnly.FromDateTime(DateTime.Now);
            var contracts = rents.Where(x => x.VehicleId == vehicle.Id && x.RentEnd > now);
            if (!contracts.Any() || !allowContracts) continue;
            foreach (var contract in contracts)
            {
                message.Append($"{Arabic.CarDetails.Contract}: /cont{contract.Id}");
                message.Append("\n");
            }
        }

        _client.SendMessageAsync(update.ChatId(), message.ToString().PadRight(3));
        return Task.CompletedTask;
    }

    public async Task RemoveVehicle(Update? update)
    {
        var v = await ChooseVehicle(update, true);
        await _vehicleManager.Remove(v.v);
    }
}