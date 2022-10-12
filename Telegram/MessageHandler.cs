using Models.DataModels;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Command;
using Telegram.Languages;

namespace Telegram;

public class MessageHandler
{
    private readonly BotClient _client;
    private Update? _update;
    private readonly OpenMessages _openMessages;
    private readonly Commander _commander;

    public MessageHandler(BotClient client, Update? update)
    {
        _client = client;
        _update = update;
        _commander = new Commander(_client, update.UserName());
        _openMessages = new OpenMessages(_client);
    }

    public async Task MainOpen()
    {
        _update = await _openMessages.Usage(_update!);
        var f = true;
        while (f)
        {
            f = false;
            switch (_update.Text())
            {
                case "/admin":
                    await Admin(await _openMessages.Admin(_update));
                    break;
                case "/addrent":
                    await _commander.AddRent(_update);
                    break;

                case "/editrent":
                    await _commander.EditRent(_update);
                    break;

                case "/cancelrent":
                    await _commander.CancelRent(_update);
                    break;

                case "/editcontract":
                    await _commander.EditContract(_update);
                    break;

                case "/showcarsnotinrent":
                    await _commander.ShowCars(Status.Completed, _update);
                    break;

                case "/showcarsinrent":
                    await _commander.ShowCars(Status.Waiting, _update);
                    break;

                case "/showall":
                    await _commander.ShowCars(Status.Cancelled, _update);
                    break;

                case "/fine":
                    await _commander.Fine(_update);
                    break;
                case "/mntnc":
                    await _commander.RegularMaintenance(_update);
                    break;
                case "/cmntnc":
                    await _commander.CycleMaintenance(_update);
                    break;
                case "/report":
                    await _commander.Report(_update);
                    break;
                case "/complete":
                    await _commander.Complete(_update);
                    break;
                default:
                    f = true;
                    await _client.SendMessageAsync(_update.ChatId(), Arabic.EntreValidOption);
                    await _client.SendMessageAsync(_update.ChatId(), Arabic.Usage);
                    _update = await _client.MessageWatcher(_update);
                    break;
            }
        }
    }


    private async Task Admin(Update? update)
    {
        var f = true;
        while (f)
        {
            f = false;
            switch (update.Text())
            {
                case "/add":
                    await _commander.AddVehicle(update ?? throw new ArgumentNullException(nameof(update)));
                    await _client.SendMessageAsync(update.ChatId(), Arabic.CarAdded);
                    break;
                case "/list":
                    await _commander.ListVehicles(update);
                    break;
                case "/remove":
                    await _commander.RemoveVehicle(update);
                    await _client.SendMessageAsync(update.ChatId(), Arabic.CarDeleted);
                    break;
                default:
                    f = true;
                    await _client.SendMessageAsync(_update.ChatId(), Arabic.EntreValidOption);
                    update = await _openMessages.Admin(_update);
                    break;
            }
        }

        _update = await _client.MessageWatcher();
        await MainOpen();
    }
}