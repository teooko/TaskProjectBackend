namespace TaskProjectBackend.Application.DTO;

public class TaskDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public TimeSpan? Time { get; set; } = TimeSpan.Zero;
}