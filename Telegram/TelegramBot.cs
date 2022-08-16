using Telegram;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.GettingUpdates;

class TelegramClient
{
    private static void Main()
    {
        var botToken = "5576522039:AAEnLj4yObQsR6O6i8R-rIxfvR66lIYsUWU";
        var api = new BotClient(botToken);


        var updates = api.GetUpdates();
        while (true)
        {
            if (updates.Any())
            {
                foreach (Update? update in updates)
                {
                    Console.WriteLine(update.Message);
                    api.SendMessage(update.ChatId(), update.Text()); // Send a message
                }

                var offset = updates.Last().UpdateId + 1;
                updates = api.GetUpdates(offset);
            }
            else
            {
                updates = api.GetUpdates();
            }
        }
    }

}
