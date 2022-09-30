using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Languages;

namespace Telegram.Command;

public partial class Commander
{
    private async Task<Update> GenericOptions(Update update, IEnumerable<string> options, string text)
    {
        await _client.SendMessageAsync(update.ChatId(), text);
        var keyboardOptions = await TextGenerator.KeyboradOptions(options);
        await _client.SendMessageAsync(update.ChatId(), Arabic.ChooseOrAdd, replyMarkup: keyboardOptions);
        update = await _client.MessageWatcher(update);
        return update;
    }
}