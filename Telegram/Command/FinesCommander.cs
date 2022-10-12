using System.Linq.Expressions;
using Models.DataModels;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Languages;

namespace Telegram.Command;

public partial class Commander
{
    internal enum AdditionType
    {
        Fine,
        Maintenance,
    }


    private async Task AddType(Update update, AdditionType additionType, TypeOfMaintenance? typeOfMaintenance = null)
    {
        await _client.SendMessageAsync(update.ChatId(), Arabic.EnterPrice);
        (update, var price) = await ReadPrice(update);
        (update, var image) = await ReadPicture(update);
        var (vehicle, _) = await ChooseVehicle(update, true, false, false, x => x.Fines, x => x.Maintenances);
        switch (additionType)
        {
            case AdditionType.Fine:
                var fine = new Fine
                {
                    Vehicle = vehicle,
                    Bill = new()
                    {
                        Image = image,
                        Price = price
                    },
                    Creation = _creator
                };
                vehicle.Fines.Add(fine);
                break;
            case AdditionType.Maintenance:
                var maintenance = new Maintenance
                {
                    Type = typeOfMaintenance!.Value,
                    Vehicle = vehicle,
                    Bill = new Bill
                    {
                        Image = image,
                        Price = price
                    },
                    Creation = _creator
                };
                vehicle.Maintenances.Add(maintenance);
                break;
        }

        await _vehicleManager.Save();
    }


    public async Task Fine(Update update) => await AddType(update, AdditionType.Fine);

    public async Task RegularMaintenance(Update update) =>
        await AddType(update, AdditionType.Maintenance, TypeOfMaintenance.Regular);

    public async Task CycleMaintenance(Update update) =>
        await AddType(update, AdditionType.Maintenance, TypeOfMaintenance.Cycle);
}