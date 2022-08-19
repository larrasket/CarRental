using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Languages;

namespace Telegram;

// TODO Contract handler 
public static class TelegramClient
{
    private static async Task Main()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var botToken = "5576522039:AAEnLj4yObQsR6O6i8R-rIxfvR66lIYsUWU";
        var botClient = new BotClient(botToken);
        var exists = Directory.Exists(ExtHelpers.media);
        var cancel = false;
        if (!exists)
            Directory.CreateDirectory(ExtHelpers.media);

        var cancelCommand = new BotCommand("/cancel", "/cancel");
        botClient.SetMyCommands(cancelCommand);

        while (true)
        {
            try
            {
                var firstUpdate = await botClient.MessageWatcher(start: true);
                if (!string.IsNullOrEmpty(firstUpdate.Text()))
                    await new MessageHandler(botClient, firstUpdate).MainOpen();
            }
            catch (Exception e)
            {
                if (e.Message != "stop")
                {
                    // Console.WriteLine(Arabic.UnkownError);
                    throw;
                    cancel = true;
                }
            }
        }
    }
}