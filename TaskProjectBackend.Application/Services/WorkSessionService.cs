using Domain;
using TaskProjectBackend.Application.DTO;
using TaskProjectBackend.DataAccess.Repositories;

namespace TaskProjectBackend.Application.Services;

public class WorkSessionService
{
    private readonly WorkSessionRepository _workSessionRepository;
    private readonly TaskRepository _taskRepository;
    public WorkSessionService(WorkSessionRepository workSessionRepository, TaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
        _workSessionRepository = workSessionRepository;
    }

    public WorkSession Get(int id)
    {
        return _workSessionRepository.Get(id);
    }

    public WorkSession Start(int id)
    {
        return _workSessionRepository.Start(id);
    }

    public void Stop(int id)
    {
        _workSessionRepository.Stop(id);
    }

    public TimeSpan? GetDuration(int id)
    {
        WorkSession workSession = _workSessionRepository.Get(id);

        TimeSpan? duration = workSession.End - workSession.Start;

        return duration;
    }

    public WorkSession PostWorkSession(WorkSessionDTO workSessionDto)
    {
        WorkSession workSession = new WorkSession();
        workSession.Start = workSessionDto.Start;
        workSession.End = workSessionDto.End;
        workSession.Task = _taskRepository.Get(workSessionDto.TaskId);
        
        return _workSessionRepository.PostWorkSession(workSession);
    }
}