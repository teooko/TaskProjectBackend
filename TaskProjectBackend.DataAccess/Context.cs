using Domain;

namespace TaskProjectBackend.DataAccess;
using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    public DbSet<Domain.Task> Tasks { get; set; }
    
    public DbSet<WorkSession> WorkSessions { get; set; }

    public DbSet<PauseSession> PauseSessions { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
        base.OnConfiguring(optionsBuilder);
    }
}