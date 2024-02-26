﻿using Domain;

namespace TaskProjectBackend.DataAccess.Repositories;

public class WorkSessionRepository
{
    public WorkSession Get(int id)
    {
        var context = new Context();
        return context.worksessions.Single(p => p.Id == id);
    }
    public void Start(int id)
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
}