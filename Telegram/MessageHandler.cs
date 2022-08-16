using Models.DataModels;
using Telegram.BotAPI;
using Telegram.BotAPI.GettingUpdates;

namespace Telegram;

public class MessageHandler
{
    private readonly BotClient _client;
    private readonly Update? _update;
    private readonly OpenMessages _openMessages;

    public MessageHandler(BotClient client, Update? update)
    {
        _client = client;
        _update = update;
        _openMessages = new OpenMessages(_client);
    }

    public async Task HandleOpen()
    {
        switch (await _update.Text())
        {
            case "/admin":
                await Admin(await _openMessages.Admin(_update));
                break;
        }
    }

    private async Task Admin(Update? action)
    {
        switch (await _update.Text())
        {
            case "/add":
                Vehicle vehicle = DataReader.Vehicle(_update);
        }
    }
}