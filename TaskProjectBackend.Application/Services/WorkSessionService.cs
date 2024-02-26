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

    public void Post(int id)
    {
        _workSessionRepository.Post(id);
    }
}