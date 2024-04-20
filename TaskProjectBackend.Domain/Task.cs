using Microsoft.AspNetCore.Identity;

namespace Domain;

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; } = "Unnamed task";
    public string Color { get; set; } = "#B53535";
    public ICollection<WorkSession>? WorkSessions { get; set; }
    public string UserId { get; set; }
    public virtual IdentityUser User { get; set; }
}
