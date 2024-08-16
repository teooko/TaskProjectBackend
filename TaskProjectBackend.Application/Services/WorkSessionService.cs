using Domain;
using TaskProjectBackend.DataAccess.Repositories;

namespace TaskProjectBackend.Application.Services;

public class WorkSessionService
{
    private readonly WorkSessionRepository _workSessionRepository;

    public WorkSessionService(WorkSessionRepository workSessionRepository)
    {
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

    public WorkSession PostWorkSession(WorkSession workSession)
    {
        return _workSessionRepository.PostWorkSession(workSession);
    }
}