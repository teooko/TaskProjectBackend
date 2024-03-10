using Domain;
using Microsoft.EntityFrameworkCore;

namespace TaskProjectBackend.DataAccess.Repositories;

public class WorkSessionRepository
{
    public WorkSession Get(int id)
    {
        var context = new Context();
        return context.worksessions.Single(p => p.Id == id);
    }
    public WorkSession Start(int id)
    {
        var context = new Context();
        WorkSession workSession = new WorkSession();
        
        DateTime currentTimeStamp = DateTime.Now;
        
        workSession.Start = currentTimeStamp;
        Domain.Task task = context.tasks.Single(t => t.Id == id);
        workSession.Task = task;
        workSession.End = null;
        
        context.Add(workSession);
        context.SaveChanges();
        
        return context.worksessions.Single(ws => ws.Start == currentTimeStamp);
    }

    public void Stop(int id)
    {
        var context = new Context();
        WorkSession workSession = context.worksessions.Single(s => s.Id == id);

        DateTime currentTimeStamp = DateTime.Now;
        workSession.End = currentTimeStamp;

        context.Update(workSession);
        context.SaveChanges();
    }
    public List<WorkSession> GetWorkSessionsByDay(DateTime date)
    {
        using var context = new Context();
        List<WorkSession> workSessions = context.worksessions
            .Include(s => s.Task)
            .Where(s => s.End.Value.Date == date.Date)
            .ToList();
        
        return workSessions;
    }
}