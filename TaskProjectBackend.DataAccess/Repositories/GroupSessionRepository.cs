using Domain;

namespace TaskProjectBackend.DataAccess.Repositories;

public class GroupSessionRepository
{
    private readonly Context _context;
    public GroupSessionRepository(Context context)
    {
        _context = context;
    }

    public GroupSession Post(string userId)
    {
        GroupSession groupSession = new GroupSession();
        groupSession.UserId1 = userId;
        _context.Add(groupSession);
        _context.SaveChanges();
        return groupSession;
    }

    public GroupSession Patch(int groupSessionId)
    {
        GroupSession groupSession = _context.groupsessions.Single(g => g.Id == groupSessionId);
        groupSession.Active = false;
        
        _context.Update(groupSession);
        _context.SaveChanges();
        
        return groupSession;
    }
    
    public GroupSession AddUser(int groupSessionId, string userId)
    {
        GroupSession groupSession = _context.groupsessions.Single(g => g.Id == groupSessionId);

        if (groupSession.UserId1 == null)
            groupSession.UserId1 = userId;
        else if (groupSession.UserId2 == null)
            groupSession.UserId2 = userId;
        else if (groupSession.UserId3 == null)
            groupSession.UserId3 = userId;
        else if (groupSession.UserId4 == null)
            groupSession.UserId4 = userId;
        else return null;
        
        _context.Update(groupSession);
        _context.SaveChanges();
        
        return groupSession;
    }
    
    public GroupSession RemoveUser(int groupSessionId, string userId)
    {
        GroupSession groupSession = _context.groupsessions.Single(g => g.Id == groupSessionId);

        if (groupSession.UserId1 == userId)
            groupSession.UserId1 = null;
        else if (groupSession.UserId2 == userId)
            groupSession.UserId2 = null;
        else if (groupSession.UserId3 == userId)
            groupSession.UserId3 = null;
        else if (groupSession.UserId4 == userId)
            groupSession.UserId4 = null;
        else return null;
        
        _context.Update(groupSession);
        _context.SaveChanges();
        
        return groupSession;
    }
}