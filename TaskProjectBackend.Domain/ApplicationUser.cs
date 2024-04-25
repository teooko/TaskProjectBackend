using Microsoft.AspNetCore.Identity;

namespace Domain;

public class ApplicationUser : IdentityUser
{
    public string? ProfilePicturePath { get; set; }
}