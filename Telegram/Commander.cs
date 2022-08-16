using System.Text;
using Models.DataModels;
using Models.Helpers;
using Services;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Languages;
using File = Telegram.BotAPI.AvailableTypes.File;

namespace Telegram;

public class Commander
{
    private readonly BotClient _client;
    private readonly TypeManager<Vehicle> _vehicleManager = new TypeManager<Vehicle>();
    private readonly TypeManager<Rent> _rentManager = new TypeManager<Rent>();

    public Commander(BotClient client)
    {
        _client = client;
    }

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


    private async Task<Update> GenericOptions(Update update, IEnumerable<string> options, string text)
    {
        await _client.SendMessageAsync(update.ChatId(), text);
        var keyboardOptions = await TextGenerator.KeyboradOptions(options);
        await _client.SendMessageAsync(update.ChatId(), Arabic.ChooseOrAdd, replyMarkup: keyboardOptions);
        update = await _client.MessageWatcher(update);
        return update;
    }

    public Task ListVehicles(Update? update, bool admin = false)
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


            var contract = rents.FirstOrDefault(x => x.Vehicle == vehicle);
            if (contract == null) continue;


            message.Append($"{Arabic.CarDetails.Contract}: /cont{contract.Id}");
            message.Append("\n");
        }

        _client.SendMessageAsync(update.ChatId(), message.ToString().PadRight(3));
        return Task.CompletedTask;
    }

    public async Task RemoveVehicle(Update? update)
    {
        var v = await ChooseVehicle(update, true);
        await _vehicleManager.Remove(v.v);
    }

    private async Task<(Vehicle v, Update update)> ChooseVehicle(Update update, bool admin = false)
    {
        await ListVehicles(update, admin);
        await _client.SendMessageAsync(update.ChatId(), Arabic.CarDetails.EnterNumber);
        update = await _client.MessageWatcher(update);
        var selected = update.Text()[1..];
        var v = _vehicleManager.All().FirstOrDefault(x => x.Number == selected);
        while (v is null)
        {
            update = await _client.MessageWatcher(update);
            v = _vehicleManager.All().FirstOrDefault(x => x.Number == selected);
        }

        return (v, update);
    }

    public async Task AddRent(Update update)
    {
        var (vehicle, update1) = await ChooseVehicle(update, true);
        var rent = new Rent
        {
            VehicleId = vehicle.Id
        };
        update = update1;
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EnterDate);
        update = await _client.MessageWatcher(update);

        var startDate = DateTime.Today;
        while (update.Text() != "/today" && !DateTime.TryParse(update.Text(), out startDate))
        {
            await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EnterValidDate);
            update = await _client.MessageWatcher(update);
            if (update.Text() != "/today") continue;
            startDate = DateTime.Today;
            break;
        }

        rent.RentStart = DateOnly.FromDateTime(startDate);
        if (vehicle.Rents != null && !rent.ValidStartDay())
        {
            await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.NotValidStartDay);
            return;
        }


        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EnterDays);
        update = await _client.MessageWatcher(update);
        var n = 0;
        while (!int.TryParse(update.Text(), out n))
        {
            await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EnterValidPrice);
            update = await _client.MessageWatcher(update);
        }

        rent.RentEnd = rent.RentStart.AddDays(n);
        if (vehicle.Rents != null && !rent.ValidEndDay())
        {
            await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.NotValidStartDay);
            return;
        }


        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EnterPrice);
        update = await _client.MessageWatcher(update);
        decimal m = 0;
        while (!decimal.TryParse(update.Text(), out m))
        {
            await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EnterValidNumber);
            update = await _client.MessageWatcher(update);
        }

        rent.Contract.Price = m;


        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.SendContractPicture);
        update = await _client.MessageWatcher(update);
        var pic = (update.Message.Photo ?? throw new InvalidOperationException()).First();
        var i = await _client.GetFileByteArrayAsync((await _client.GetFileAsync(pic.FileId)).FilePath);
        await System.IO.File.WriteAllBytesAsync(Path.Combine(ExtHelpers.media, pic.FileId), i);
        rent.Contract.Image = pic.FileId;


        rent.CreatedBy = update.UserName();
        rent.Contract.CreatedBy = rent.CreatedBy;
        var rs = await _rentManager.Add(rent);
        if (rs <= 0) throw new Exception();
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.Added);
    }
}