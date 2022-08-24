using Services;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;
using Telegram.Languages;

namespace Telegram;

public static class TelegramClient
{
    private static void Init()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var exists = Directory.Exists(ExtHelpers.media);
        if (!exists)
            Directory.CreateDirectory(ExtHelpers.media);
    }

    private static async Task Main()
    {
        Init();
        var botToken = LocalReader.GetObj("PTelegramBotKey");
        var botClient = new BotClient(botToken);
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
                if (e.Message == "stop") continue;
                Console.WriteLine(Arabic.UnkownError);
                throw;
            }
        }
    }
}