using System.Text;
using Models.DataModels;
using Services;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Languages;

namespace Telegram;

public class Commander
{
    private readonly BotClient _client;

    public Commander(BotClient client) => _client = client;

    public async Task<int> Vehicle(Update update)
    {
        var vehicle = new Vehicle();
        var manager = new TypeManager<Vehicle>();

        update = await GenericOptions(update, manager.All().Select(x => x.Brand), Arabic.AddBrand);
        vehicle.Brand = update.Text();

        update = await GenericOptions(update, manager.All().Select(x => x.Model), Arabic.AddModel);
        vehicle.Model = update.Text();

        update = await GenericOptions(update, manager.All().Select(x => x.Color), Arabic.AddColor);
        vehicle.Color = update.Text();

        var numbers = manager.All().Select(x => x.Number);
        while (true)
        {
            update = await GenericOptions(update, manager.All().Select(x => x.Number), Arabic.AddNumber);
            if (numbers.Any(x => x == update.Text()) && update.Text() != "/cancel")
            {
                await _client.SendMessageAsync(update.ChatId(), Arabic.CarNumberAlreadyExist);
            }
            else if (update.Text() == "/cancel")
            {
                return -15;
            }
            else break;
        }

        vehicle.Number = update.Text();
        return await manager.Add(vehicle);
    }


    private async Task<Update> GenericOptions(Update update, IEnumerable<string> options, string text)
    {
        await _client.SendMessageAsync(update.ChatId(), text);
        var keyboradOptions = await TextGenerator.KeyboradOptions(options);
        await _client.SendMessageAsync(update.ChatId(), Arabic.ChooseOrAdd, replyMarkup: keyboradOptions);
        update = await _client.MessageWatcher(update);
        return update;
    }

    public Task ListVehicles(Update? action)
    {
        var vehicles = new TypeManager<Vehicle>().All();
        var rents = new TypeManager<Rent>().All();
        var message = new StringBuilder();
        foreach (var vehicle in vehicles)
        {
            message.Append($"{Arabic.CarDetails.Number}: {vehicle.Number}");
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

        return Task.CompletedTask;
    }
}