namespace TaskProjectBackend.Application.DTO;

public class WeeklyTasksDTO
{
    public DateTime? Day { get; set; }
    public List<string> Colors { get; set; } = new List<string>();
}