using Models.DataModels;
using Services;
using Telegram.BotAPI;

namespace Telegram.Command;

public partial class Commander
{
    private readonly BotClient _client;
    private static TelegramCreator _creator;

    public Commander(BotClient client, string username)
    {
        _client = client;
        _creator = new TelegramCreator(username);
        _vehicleManager = new (_creator);
        _rentManager = new(_creator);
    }
}