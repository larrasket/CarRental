using Models.DataModels;
using Services;

namespace Telegram;

public sealed class TelegramCreator : Creator
{
    private string _userName;

    public TelegramCreator(string userName)
    {
        _userName = userName;
        TypeSimpleManager<ClientUser> clientUsers = new();
        var clientUser = clientUsers.First(x => x.UserIdentifier == userName).Result;
        if (clientUser == null)
        {
            User = new ClientUser(clientId: Client.Telegram, userIdentifier: userName);
            clientUsers?.Add(User);
        }
        else
        {
            User = clientUser;
        }
    }
}