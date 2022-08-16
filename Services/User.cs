using Microsoft.AspNetCore.Identity;

namespace Services;

public class User : IdentityUser
{
    public Role Role;
}