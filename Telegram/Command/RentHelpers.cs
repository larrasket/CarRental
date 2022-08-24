using Models.DataModels;
using Models.Helpers;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Languages;

namespace Telegram.Command;

public partial class Commander
{
    private async Task<Update> ChooseRent(Update update)
    {
        var (vehicle, u) = await ChooseVehicle(update, contracts: true);
        update = u;
        while (!update.Text().Contains("/cont"))
        {
            await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.Cancel);
            update = await _client.MessageWatcher(update);
        }

        return update;
    }

    private async Task<(Rent rent, Update update)> ReadRentStartDate(Update update, Rent rent, Vehicle vehicle)
    {
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
        rent.Vehicle = vehicle;
        if (rent.ValidStartDay())
        {
            rent.Vehicle = null;
            return (rent, update);
        }

        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.NotValidStartDay);
        throw new Exception("stop");
    }

    private async Task<(Rent rent, Update update)> ReadRentEndDate(Update update, Rent rent, Vehicle vehicle)
    {
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EnterDays);
        update = await _client.MessageWatcher(update);
        var n = 0;
        while (!int.TryParse(update.Text(), out n))
        {
            await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EnterValidPrice);
            update = await _client.MessageWatcher(update);
        }

        rent.RentEnd = rent.RentStart.AddDays(n);
        if (vehicle.Rents == null || rent.ValidEndDay()) return (rent, update);
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.NotValidStartDay);
        throw new Exception("stop");
    }

    private async Task<(Rent rent, Update update)> ReadRentPrice(Update update, Rent rent)
    {
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EnterPrice);
        update = await _client.MessageWatcher(update);
        decimal m = 0;
        while (!decimal.TryParse(update.Text(), out m))
        {
            await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EnterValidNumber);
            update = await _client.MessageWatcher(update);
        }

        rent.Contract.Price = m;
        return (rent, update);
    }

    private async Task<(Rent rent, Update update)> ReadRentContract(Update update, Rent rent)
    {
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.ContractOptions);
        update = await _client.MessageWatcher(update);
        while (update.Text() != "/yes" && update.Text() != "/no")
        {
            await _client.SendMessageAsync(update.ChatId(), Arabic.EntreValidOption);
            update = await _client.MessageWatcher(update);
        }

        if (update.Text() == "/no")
            return (rent, update);


        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.SendContractPicture);
        update = await _client.MessageWatcher(update);
        var pic = (update.Message.Photo ?? throw new InvalidOperationException()).Last();
        var i = await _client.GetFileByteArrayAsync((await _client.GetFileAsync(pic.FileId)).FilePath);
        var filename = Guid.NewGuid().ToString()[..7] + ".jpeg";
        await File.WriteAllBytesAsync(Path.Combine(ExtHelpers.media, filename), i);

        // TODO bad perform
        var cntrct = await _contractManager.Find(rent.BillId);

        cntrct.Image = filename;
        int j = await _contractManager.Save();
        // await _contractManager.Edit(cntrct);
        return (rent, update);
    }


    private async Task<(Rent rent, Update update)> ReadRentDriver(Update update, Rent rent)
    {
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.SendDriver);
        update = await _client.MessageWatcher(update);
        var pic = (update.Message.Photo ?? throw new InvalidOperationException()).Last();
        var i = await _client.GetFileByteArrayAsync((await _client.GetFileAsync(pic.FileId)).FilePath);
        var filename = Guid.NewGuid().ToString()[..7] + ".jpeg";
        await File.WriteAllBytesAsync(Path.Combine(ExtHelpers.media, filename), i);
        rent.Driver = filename;
        return (rent, update);
    }
}