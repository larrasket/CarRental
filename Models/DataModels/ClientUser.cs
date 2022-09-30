using System.ComponentModel.DataAnnotations;

namespace Models.DataModels;

public class ClientUser
{
    public ClientUser(Client clientId, string userIdentifier)
    {
        ClientId = clientId;
        UserIdentifier = userIdentifier;
    }

    [Key] public long Id { get; set; }
    public string UserIdentifier { get; set; }
    public Client ClientId { get; set; }
    public List<Creator> CreatedThings { get; set; }
}