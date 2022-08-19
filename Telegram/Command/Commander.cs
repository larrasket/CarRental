using Telegram.BotAPI;

namespace Telegram.Command;

public partial class Commander
{
    private readonly BotClient _client;
    public Commander(BotClient client) => _client = client;
}
