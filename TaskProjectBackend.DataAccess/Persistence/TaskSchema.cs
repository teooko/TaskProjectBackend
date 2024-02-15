using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Domain.Task;

namespace TaskProjectBackend.DataAccess.Persistence;

public class TaskSchema : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(255);
        builder.Property(p => p.Color).HasColumnType("text");

        builder.HasMany(p => p.Cast)
            .WithOne(p => p.Movie)
            .HasForeignKey(c => c.MovieId);
        
    }
}