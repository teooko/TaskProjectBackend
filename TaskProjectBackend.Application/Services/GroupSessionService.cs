using Domain;
using TaskProjectBackend.DataAccess.Repositories;

namespace TaskProjectBackend.Application.Services;

public class GroupSessionService
{
    private readonly GroupSessionRepository _groupSessionRepository;
    
    public GroupSessionService(GroupSessionRepository groupSessionRepository)
    {
        _groupSessionRepository = groupSessionRepository;
    }

    public GroupSession Post(string userId)
    {
        GroupSession groupSession = _groupSessionRepository.Post(userId);
        return groupSession;
    }

    public GroupSession Patch(int groupSessionId)
    {
        GroupSession groupSession = _groupSessionRepository.Patch(groupSessionId);
        return groupSession;
    }
    
    public GroupSession Join(int groupSessionId, string userId)
    {
        GroupSession groupSession = _groupSessionRepository.AddUser(groupSessionId, userId);
        return groupSession;
    }
    
    public GroupSession Leave(int groupSessionId, string userId)
    {
        GroupSession groupSession = _groupSessionRepository.RemoveUser(groupSessionId, userId);
        return groupSession;
    }
}