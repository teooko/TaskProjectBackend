using Domain;
using TaskProjectBackend.DataAccess.Repositories;

namespace TaskProjectBackend.Application.Services;

public class WorkSessionService
{
    private readonly WorkSessionRepository _workSessionRepository;

    public WorkSessionService()
    {
        _workSessionRepository = new WorkSessionRepository();
    }

    public WorkSession Get(int id)
    {
        return _workSessionRepository.Get(id);
    }

    public void Start(int id)
    {
        _workSessionRepository.Start(id);
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
}