namespace Domain;

public class WorkSession : Session
{
    public virtual Task? Task { get; set; }
    public ICollection<PauseSession>? PauseSessions { get; set; }
}