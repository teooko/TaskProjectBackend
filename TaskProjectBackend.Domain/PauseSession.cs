namespace Domain;

public class PauseSession : Session
{
    public virtual WorkSession? WorkSession { get; set; }
}