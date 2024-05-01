namespace Domain;

public class GroupSession
{
    public int Id { get; set; }
    public bool Active { get; set; } = true;
    
    public string? UserId1 { get; set; }
    public virtual ApplicationUser? User1 { get; set; }
    
    public string? UserId2 { get; set; }
    public virtual ApplicationUser? User2 { get; set; }
    
    public string? UserId3 { get; set; }
    public virtual ApplicationUser? User3 { get; set; }
    
    public string? UserId4 { get; set; }
    public virtual ApplicationUser? User4 { get; set; }
    
}