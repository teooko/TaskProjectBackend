namespace TaskProjectBackend.DataAccess.Repositories;
using Domain;

public class TaskRepository
{
    public Task Get(int id)
    {
        using var context = new Context();
        return context.Tasks.Single(p => p.Id == id);
    }

    public Task Post(Task task)
    {
        using var context = new Context();
        context.Add(task);
        context.SaveChanges();
        return this.Get(task.Id);
    }

}