using Telegram.BotAPI.AvailableTypes;

namespace Telegram;

public static class TextGenerator
{
    public static Task<ReplyKeyboardMarkup> KeyboradOptions(IEnumerable<string> names)
    {
        var options = names.Select(brand => new List<KeyboardButton>(new[] {new KeyboardButton(brand)})).ToList();
        return Task.FromResult(new ReplyKeyboardMarkup(options));
    }
}