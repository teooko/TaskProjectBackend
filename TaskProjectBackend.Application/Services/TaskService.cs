using Domain;
using TaskProjectBackend.DataAccess.Repositories;

namespace TaskProjectBackend.Application.Services;

public class TaskService
{
    private readonly TaskRepository _taskRepository;
    private readonly WorkSessionRepository _workSessionRepository;
    public TaskService()
    {
        _taskRepository = new TaskRepository();
        _workSessionRepository = new WorkSessionRepository();
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

    public List<Domain.Task> GetTasksByDay(DateTime date)
    {
        List<Domain.Task> tasks = new List<Domain.Task>();
        List<WorkSession> workSessions = _workSessionRepository.GetWorkSessionsByDay(date);
        
        foreach (var workSession in workSessions)
        {
            
            if (!tasks.Any(t => t.Id == workSession.Task.Id))
            {
                Domain.Task task = new Domain.Task();
                task.Id = workSession.Task.Id;
                task.Color = workSession.Task.Color;
                task.Name = workSession.Task.Name;
                
                tasks.Add(task);
            }
            
        }
        return tasks;
    }
}