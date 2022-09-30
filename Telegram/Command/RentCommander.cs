using Models.DataModels;
using Services;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Languages;

namespace Telegram.Command;

public partial class Commander
{
    private readonly DataManager<Rent> _rentManager;

    public async Task AddRent(Update update)
    {
        var (vehicle, update1) = await ChooseVehicle(update, true, includeExp: x => x.Rents);
        var rent = new Rent
        {
            VehicleId = vehicle.Id,
            Contract = new Bill(),
        };
        update = update1;

        (rent, update) = await AddStartDate(update, rent, vehicle);
        (rent, update) = await AddEndDate(update, rent, vehicle);
        (rent, update) = await ReadRentPrice(update, rent);
        (rent, update) = await ReadRentContract(update, rent);
        (rent, update) = await ReadRentDriver(update, rent);
        // rent.CreatedBy = update.UserName();
        // rent.Contract.CreatedBy = rent.CreatedBy;
        rent.Status = Status.Waiting;
        var rs = await _rentManager.Add(rent);
        if (rs <= 0) throw new Exception();
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.Added);
    }

    public async Task CancelRent(Update update)
    {
        update = await ChooseRent(update);
        var continued = update.Text()[5..];
        var rent = await _rentManager.First(x => x.Id == long.Parse(continued));
        rent.Status = Status.Cancelled;
        rent.RentStart = rent.RentEnd = DateOnly.MinValue;
        await _rentManager.Save();
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.Cancelled);
    }

    public async Task EditContract(Update update)
    {
        update = await ChooseRent(update);
        var continued = update.Text()[5..];
        var rent = await _rentManager.Find(long.Parse(continued), x => x.Contract);
        await ReadRentContract(update, rent ?? throw new InvalidOperationException(), true);
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.Edited);
    }

    public async Task EditRent(Update update)
    {
        update = await ChooseRent(update);
        var continued = update.Text()[5..];
        var rent = await _rentManager.First(x => x.Id == long.Parse(continued), x => x.Vehicle);
        if (rent == null) throw new NullReferenceException();

        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EditStartOrEnd);
        update = await _client.MessageWatcher(update);
        while (update.Text() != "/start" && update.Text() != "/end")
        {
            await _client.SendMessageAsync(update.ChatId(), Arabic.EntreValidOption);
            update = await _client.MessageWatcher(update);
        }

        if (update.Text() == "/start")
        {
            (_, rent.RentStart) = await ReadStartDate(update);
        }
        else
        {
            int n = -1;
            (_, n) = await ReadEndDate(update);
            rent.RentEnd = rent.RentStart.AddDays(n);
        }

        int f = await _rentManager.Save();
        Console.WriteLine(f);
    }
}