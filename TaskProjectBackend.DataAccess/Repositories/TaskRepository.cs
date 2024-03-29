﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace TaskProjectBackend.DataAccess.Repositories;
using Domain;

public class TaskRepository
{
    public Task Get(int id)
    {
        using var context = new Context();
        return context.tasks.Single(p => p.Id == id);
    }

    public List<Task> Get()
    {
        using var context = new Context();
        return context.tasks.ToList();
    }
    
    public Task Post(Task task)
    {
        using var context = new Context();
        context.Add(task);
        context.SaveChanges();
        return this.Get(task.Id);
    }

    public Task Delete(int id)
    {
        using var context = new Context();
        var task = this.Get(id);
        
        List<WorkSession> workSessions = this.GetWorkSessions(id);
        foreach (var workSession in workSessions)
        {
            context.Remove(workSession);
        }
        context.SaveChanges();
        
        context.Remove(task);
        context.SaveChanges();
        return task;
    }

    public List<WorkSession> GetWorkSessions(int id)
    {
        using var context = new Context();
        List<WorkSession> workSessions = context.worksessions.Where(s => s.Task.Id == id).ToList();

        return workSessions;
    }

    public List<WorkSession> GetWeeklyWorkSessions(int fromDate)
    {
        using var context = new Context();
        List<WorkSession> workSessions = new List<WorkSession>();
        for (int i = fromDate; i < fromDate + 7; i++)
        {
            DateTime dateTime = DateTime.Today.AddDays(-i);
            List<WorkSession> newWorkSessions = context.worksessions
                .Where(e => e.End.Value.Date == dateTime.Date)
                .Include(e => e.Task)
                .ToList();
            workSessions.AddRange(newWorkSessions);
        }

        return workSessions;
    }
    
}