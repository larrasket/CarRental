using Telegram.BotAPI;
using Telegram.BotAPI.GettingUpdates;

namespace Telegram.Command;

public partial class Commander
{
    private readonly BotClient _client;
    
    public Commander(BotClient client) => _client = client;
}