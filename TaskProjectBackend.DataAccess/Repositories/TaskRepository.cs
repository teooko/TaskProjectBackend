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

    public List<Task> Get()
    {
        //using var context = new Context();
        return _context.tasks.ToList();
    }
    
    public Task Post(Task task)
    {
        //using var context = new Context();
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

    public List<WorkSession> GetWeeklyWorkSessions(int fromDate)
    {
        //using var context = new Context();
        List<WorkSession> workSessions = new List<WorkSession>();
        for (int i = fromDate; i < fromDate + 7; i++)
        {
            DateTime dateTime = DateTime.Today.AddDays(-i);
            List<WorkSession> newWorkSessions = _context.worksessions
                .Where(e => e.End.Value.Date == dateTime.Date)
                .Include(e => e.Task)
                .ToList();
            workSessions.AddRange(newWorkSessions);
        }

        return workSessions;
    }
    
    public List<WorkSession> GetMonthlyWorkSessions(int monthsAgo)
    {
        //using var context = new Context();
        
            List<WorkSession> workSessions = new List<WorkSession>();
            DateTime endDate = DateTime.Today; // Current date
            DateTime startDate = endDate.AddMonths(-monthsAgo).AddDays(-endDate.Day + 1); // Start date 6 months ago

            // Loop through each month within the specified range
            while (startDate <= endDate)
            {
                DateTime nextMonthStartDate = startDate.AddMonths(1); // Start of next month

                // Fetch work sessions within the current month
                List<WorkSession> newWorkSessions = _context.worksessions
                    .Where(e => e.End.HasValue && e.End.Value.Date >= startDate && e.End.Value.Date < nextMonthStartDate)
                    .Include(e => e.Task)
                    .ToList();

                workSessions.AddRange(newWorkSessions);

                // Move to the start of the next month
                startDate = nextMonthStartDate;
            }

            return workSessions;
        
    }

    
}