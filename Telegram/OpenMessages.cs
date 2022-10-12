using Telegram.Bot.Types;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;
using Telegram.Languages;
using Update = Telegram.BotAPI.GettingUpdates.Update;

namespace Telegram;

public class OpenMessages
{
    private BotClient _client;

    public OpenMessages(BotClient client) => _client = client;

    public async Task<Update?> Admin(Update? update)
    {
        await _client.SendMessageAsync(update.ChatId(), Arabic.AdminUsage, replyMarkup: new ReplyKeyboardMarkup(null));
        update = await _client.MessageWatcher(update);
        return update;
    }

    public async Task<Update> Usage(Update update)
    {
        await _client.SendMessageAsync(update.ChatId(), Arabic.Usage, replyMarkup: new ReplyKeyboardMarkup(null));
        if (update != null)
            update = await _client.MessageWatcher(update);
        else
            update = await _client.MessageWatcher();
        return update;
    }
}