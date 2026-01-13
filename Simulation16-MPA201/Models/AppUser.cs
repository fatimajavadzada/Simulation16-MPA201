using Microsoft.AspNetCore.Identity;

namespace Simulation16_MPA201.Models;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; } = null!;
}
