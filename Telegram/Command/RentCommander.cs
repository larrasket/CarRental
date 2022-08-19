using Models.DataModels;
using Services;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Languages;

namespace Telegram.Command;

public partial class Commander
{
    private readonly TypeManager<Rent> _rentManager = new();
    private readonly TypeManager<Bill> _contractManager = new();

    public async Task AddRent(Update update)
    {
        var (vehicle, update1) = await ChooseVehicle(update, true);
        var rent = new Rent
        {
            VehicleId = vehicle.Id
        };
        update = update1;

        (rent, update) = await ReadRentStartDate(update, rent, vehicle);
        (rent, update) = await ReadRentEndDate(update, rent, vehicle);
        (rent, update) = await ReadRentPrice(update, rent);
        (rent, update) = await ReadRentContract(update, rent);

        rent.CreatedBy = update.UserName();
        rent.Contract.CreatedBy = rent.CreatedBy;

        var rs = await _rentManager.Add(rent);
        if (rs <= 0) throw new Exception();
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.Added);
    }

    public async Task CancelRent(Update update)
    {
        update = await ChooseRent(update);
        var continued = update.Text()[5..];
        var rent = _rentManager.Going().First(x => x.Id == long.Parse(continued));
        rent.Status = Status.Cancelled;
        await _rentManager.Edit(rent);
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.Cancelled);
    }

    public async Task EditContract(Update update)
    {
        update = await ChooseRent(update);
        var continued = update.Text()[5..];
        var rent = _rentManager.Going().First(x => x.Id == long.Parse(continued));
        (rent, _) = await ReadRentContract(update, rent);
        await _rentManager.Edit(rent);
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.Edited);
    }
}