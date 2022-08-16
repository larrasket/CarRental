using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
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
        _commander = new Commander(_client);
        _openMessages = new OpenMessages(_client);
    }

    public async Task MainOpen(bool start = false)
    {
        // if (!start)
        _update = await _openMessages.Usage(_update);
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

// TODO Call Main 
        _update = await _client.MessageWatcher();
        await MainOpen();
    }
}