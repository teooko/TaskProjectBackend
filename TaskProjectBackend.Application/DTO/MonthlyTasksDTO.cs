using Org.BouncyCastle.Asn1.Cms;

namespace TaskProjectBackend.Application.DTO;

public class MonthlyTasksDTO
{
    public int MonthNumber { get; set; }
    public TimeSpan? Time { get; set; } = TimeSpan.Zero;
}