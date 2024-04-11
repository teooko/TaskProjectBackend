namespace TaskProjectBackend.Application.DTO;

public class TotalTasksTimeDTO
{
    public int Id { get; set; }
    public TimeSpan? Time { get; set; } = TimeSpan.Zero;
}