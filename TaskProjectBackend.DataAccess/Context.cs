using Domain;

namespace TaskProjectBackend.DataAccess;
using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    public DbSet<Domain.Task> tasks { get; set; }
    
    public DbSet<WorkSession> worksessions { get; set; }

    public DbSet<PauseSession> pausesessions { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;database=mydatabase;password=password");

        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Task>().ToTable("tasks");
            modelBuilder.Entity<WorkSession>().ToTable("worksessions");
            modelBuilder.Entity<PauseSession>().ToTable("pausesessions");
        }
    
}