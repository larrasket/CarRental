using System.Net;
using Models.DataModels;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;

namespace Telegram;

public static class ExtHelpers
{
    public static long ChatId(this Update? update) => update!.Message.Chat.Id;
    public static string Text(this Update? update) => update?.Message.Text!;

    public static string UserName(this Update? update)
    {
        if (update == null)
            throw new ArgumentException();
        return update.Message.Chat.FirstName;
    }

    public const string media = "media"; // Your code goes here

    public static async Task<Update> MessageWatcher(this BotClient botClient, Update? update = null, bool start = false)
    {
        Update[]? updates;
        if (update is not null)
            updates = await botClient.GetUpdatesAsync(update.UpdateId + 1);
        else
            updates = await botClient.GetUpdatesAsync();

        while (true)
            if (updates.Any())
            {
                var msg = updates.Last().Text();
                if (!string.IsNullOrEmpty(msg))
                {
                    var index = msg.IndexOf("@", StringComparison.Ordinal);
                    if (index >= 0)
                    {
                        msg = msg[..index];
                        updates.Last().Message.Text = msg;
                    }
                }


                if (msg != "/cancel")
                    return updates.Last();
                updates.Last().Message.Text = ".";
                if (!start)
                    throw new Exception("stop");
            }
            else
                updates = await botClient.GetUpdatesAsync();
    }

    public static byte[] GetFileByteArray(this BotClient bot, string filePath)
    {
        if (bot == default)
        {
            throw new ArgumentNullException(nameof(bot));
        }

        var request = $"https://api.telegram.org/bot{bot.Token}/{filePath}";
        using var httpClient = new HttpClient();
        return httpClient.GetByteArrayAsync(request).Result;
    }


    public static async Task<byte[]> GetFileByteArrayAsync(this BotClient bot, string filePath)
    {
        if (bot == default)
        {
            throw new ArgumentNullException(nameof(bot));
        }

        var request = $"https://api.telegram.org/file/bot{bot.Token}/{filePath}";
        using var httpClient = new HttpClient();
        return await httpClient.GetByteArrayAsync(request);
    }
}