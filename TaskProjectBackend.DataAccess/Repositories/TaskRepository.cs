using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace TaskProjectBackend.DataAccess.Repositories;
using Domain;

public class TaskRepository
{
    private readonly Context _context;
    
    public TaskRepository(Context context)
    {
        _context = context;
    }
    public Task Get(int id)
    {
        //using var context = new Context();
        return _context.tasks.Single(p => p.Id == id);
    }

    public List<Task> Get(string userId)
    {
        //using var context = new Context();
        return _context.tasks.Where(t => t.UserId == userId).ToList();
    }
    
    public Task Post(Task task)
    {
        _context.Add(task);
        _context.SaveChanges();
        return this.Get(task.Id);
    }

    public Task Delete(int id)
    {
        //using var context = new Context();
        var task = this.Get(id);
        
        List<WorkSession> workSessions = this.GetWorkSessions(id);
        foreach (var workSession in workSessions)
        {
            _context.Remove(workSession);
        }
        _context.SaveChanges();
        
        _context.Remove(task);
        _context.SaveChanges();
        return task;
    }

    public List<WorkSession> GetWorkSessions(int id)
    {
        //using var context = new Context();
        List<WorkSession> workSessions = _context.worksessions.Where(s => s.Task.Id == id).ToList();

        return workSessions;
    }

    public List<WorkSession> GetWeeklyWorkSessions(string userId, int fromDate)
    {
        List<WorkSession> workSessions = new List<WorkSession>();
        for (int i = fromDate; i < fromDate + 7; i++)
        {
            DateTime dateTime = DateTime.Today.AddDays(-i);
            List<WorkSession> newWorkSessions = _context.worksessions
                .Where(e => e.End.Value.Date == dateTime.Date && e.Task.UserId == userId)
                .Include(e => e.Task)
                .ToList();
            workSessions.AddRange(newWorkSessions);
        }

        return workSessions;
    }
    
    public List<WorkSession> GetMonthlyWorkSessions(string userId, int monthsAgo)
    {
        //using var context = new Context();
        
            List<WorkSession> workSessions = new List<WorkSession>();
            DateTime endDate = DateTime.Today; // Current date
            DateTime startDate = endDate.AddMonths(-monthsAgo).AddDays(-endDate.Day + 1); // Start date 6 months ago
            
                // Fetch work sessions within the current month
                List<WorkSession> newWorkSessions = _context.worksessions
                    .Where(e => e.End.HasValue && e.End.Value.Date >= startDate && e.Task.UserId == userId)
                    .Include(e => e.Task)
                    .ToList();
                Console.WriteLine(newWorkSessions.Count() + "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZz");



                return newWorkSessions;
        
    }

    
}