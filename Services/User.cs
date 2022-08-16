using Microsoft.AspNetCore.Identity;

namespace Services;

public class User : IdentityUser
{
    public Roles Roles { get; set; }
}