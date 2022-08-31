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
        var (vehicle, u) = await ChooseVehicle(update, rents: true);
        update = u;
        while (!update.Text().Contains("/cont") && !update.Text().Contains("/rent"))
        {
            await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.Cancel);
            update = await _client.MessageWatcher(update);
        }

        return update;
    }


    private async Task<(Update update, DateOnly)> ReadStartDate(Update update)
    {
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EnterDate);
        update = await _client.MessageWatcher(update);
        var startDate = DateTime.Today;
        while (update.Text() != "/today" && !DateTime.TryParseExact(update.Text(), "yyyy-MM-dd",
                   System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,
                   out startDate))
        {
            await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EnterValidDate);
            update = await _client.MessageWatcher(update);
            if (update.Text() != "/today") continue;
            startDate = DateTime.Today;
            break;
        }

        return (update, DateOnly.FromDateTime(startDate));
    }

    private async Task<(Rent rent, Update update)> AddStartDate(Update update, Rent rent, Vehicle vehicle)
    {
        (update, rent.RentStart) = await ReadStartDate(update);
        rent.Vehicle = vehicle;
        if (rent.ValidStartDay())
        {
            rent.Vehicle = null;
            return (rent, update);
        }

        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.NotValidStartDay);
        throw new Exception("stop");
    }


    private async Task<(Update update, int)> ReadEndDate(Update update)
    {
        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EnterDays);
        update = await _client.MessageWatcher(update);
        var n = 0;
        while (!int.TryParse(update.Text(), out n))
        {
            await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.EnterValidPrice);
            update = await _client.MessageWatcher(update);
        }

        return (update, n);
    }

    private async Task<(Rent rent, Update update)> AddEndDate(Update update, Rent rent, Vehicle vehicle)
    {
        var n = -1;
        (update, n) = await ReadEndDate(update);
        rent.RentEnd = rent.RentStart.AddDays(n);
        rent.Vehicle = vehicle;
        if (rent.ValidEndDay())
        {
            rent.Vehicle = null;
            return (rent, update);
        }

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

    private async Task<(Rent rent, Update update)> ReadRentContract(Update update, Rent rent, bool read = false)
    {
        if (!read)
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
        }


        await _client.SendMessageAsync(update.ChatId(), Arabic.Rent.SendContractPicture);
        update = await _client.MessageWatcher(update);
        var pic = (update.Message.Photo ?? throw new InvalidOperationException()).Last();
        var i = await _client.GetFileByteArrayAsync((await _client.GetFileAsync(pic.FileId)).FilePath);
        var filename = Guid.NewGuid().ToString()[..7] + ".jpeg";
        await File.WriteAllBytesAsync(Path.Combine(ExtHelpers.media, filename), i);

        // // TODO bad perform
        // var cntrct = await _contractManager.Find(rent.BillId);
        // if(cntrct == null)  cntrct = new Bill()
        // {
        //     
        // }
        // int j = await _contractManager.Save();
        // // await _contractManager.Edit(cntrct);
        rent.Contract.Image = filename;
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