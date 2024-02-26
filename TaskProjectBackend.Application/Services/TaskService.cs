using Domain;
using TaskProjectBackend.DataAccess.Repositories;

namespace TaskProjectBackend.Application.Services;

public class TaskService
{
    private readonly TaskRepository _taskRepository;

    public TaskService()
    {
        _taskRepository = new TaskRepository();
    }
    public List<Domain.Task> Get()
    {
        var tasks = _taskRepository.Get();
        return tasks;
    }
    public Domain.Task Get(int id)
    {
        var task = _taskRepository.Get(id);
        return task;
    }
    public Domain.Task Post(Domain.Task task)
    {
        return _taskRepository.Post(task);
    }
    public Domain.Task Delete(int id)
    {
        return _taskRepository.Delete(id);
    }

    public TimeSpan? GetDuration(int id)
    {
        List<WorkSession> workSessions = _taskRepository.GetWorkSessions(id);

        TimeSpan? duration = TimeSpan.Zero;
        foreach (var workSession in workSessions)
        {
            duration += workSession.End - workSession.Start;
        }

        return duration;
    }
}