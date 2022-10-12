namespace Telegram.Command;

using Models.DataModels;
using Services;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Languages;

public partial class Commander
{
    private readonly DataManager<Vehicle> _vehicleManager;

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
        vehicle.Creation = _creator;
        var rs = await _vehicleManager.Add(vehicle);
        if (rs <= 0) throw new Exception();
        return update;
    }


    public async Task ListVehicles(Update? update, bool admin = false, bool allowContracts = false,
        bool allowRents = false)
    {
        var tday = DateOnly.FromDateTime(DateTime.Today);
        var vehicles = allowRents
            ? _vehicleManager.Where(x => x.Rents.Any(r => r.Status != Status.Cancelled && r.RentEnd >= tday),
                x => x.Rents)
            : _vehicleManager.All(x => x.Rents);
        var message = await ListBuilder(vehicles, admin, allowContracts, allowRents);
        await _client.SendMessageAsync(update.ChatId(), message.PadRight(3));
    }

    public async Task RemoveVehicle(Update? update)
    {
        var v = await ChooseVehicle(update!, true);
        await _vehicleManager.Remove(v.v);
    }
}