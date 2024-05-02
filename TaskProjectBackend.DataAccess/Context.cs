using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskProjectBackend.DataAccess;
using Microsoft.EntityFrameworkCore;

public class Context : IdentityDbContext<ApplicationUser>
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
    public DbSet<Domain.Task> tasks { get; set; }
    
    public DbSet<WorkSession> worksessions { get; set; }

    public DbSet<PauseSession> pausesessions { get; set; }
    public DbSet<GroupSession> groupsessions { get; set; }
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;database=mydatabase;password=password");

        base.OnConfiguring(optionsBuilder);
    }
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("aspnetusers");
            modelBuilder.Entity<Domain.Task>().ToTable("tasks");
            modelBuilder.Entity<WorkSession>().ToTable("worksessions");
            modelBuilder.Entity<PauseSession>().ToTable("pausesessions");
            modelBuilder.Entity<GroupSession>().ToTable("groupsessions");
        }
    
}