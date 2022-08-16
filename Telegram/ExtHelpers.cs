using System.Runtime.CompilerServices;
using Telegram.BotAPI.GettingUpdates;

namespace Telegram;

public static class Helpers
{
    public static long ChatId(this Update update) => update.Message.Chat.Id;
    public static string Text(this Update update) => update.Message.Text!;
}