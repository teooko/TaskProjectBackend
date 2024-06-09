using Domain;
using Microsoft.EntityFrameworkCore;

namespace TaskProjectBackend.DataAccess.Repositories;

public class WorkSessionRepository
{
    private readonly Context _context;
    
    public WorkSessionRepository(Context context)
    {
        _context = context;
    }
    public WorkSession Get(int id)
    {
        //var context = new Context();
        return _context.worksessions.Single(p => p.Id == id);
    }
    public WorkSession Start(int id)
    {
        //var context = new Context();
        WorkSession workSession = new WorkSession();
        
        DateTime currentTimeStamp = DateTime.Now;
        
        workSession.Start = currentTimeStamp;
        Domain.Task task = _context.tasks.Single(t => t.Id == id);
        workSession.Task = task;
        workSession.End = null;
        
        _context.Add(workSession);
        _context.SaveChanges();
        
        return _context.worksessions.Single(ws => ws.Start == currentTimeStamp);
    }

    public void Stop(int id)
    {
        //var context = new Context();
        WorkSession workSession = _context.worksessions.Single(s => s.Id == id);

        DateTime currentTimeStamp = DateTime.Now;
        workSession.End = currentTimeStamp;

        _context.Update(workSession);
        _context.SaveChanges();
    }
    public List<WorkSession> GetWorkSessionsByDay(string userId, DateTime date)
    {
        List<WorkSession> workSessions = _context.worksessions
            .Include(s => s.Task)
            .Where(s => s.End.Value.Date == date.Date  && s.Task.UserId == userId)
            .ToList();
        
        return workSessions;
    }
    
    public List<WorkSession> GetAllWorkSessions()
    {
        List<WorkSession> workSessions = _context.worksessions.Include(s => s.Task)
            .ToList();
        
        return workSessions;
    }
}