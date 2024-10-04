using System.Text.RegularExpressions;
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
        return _context.tasks.Single(p => p.Id == id);
    }

    public List<Task> Get(string userId)
    {
        return _context.tasks.Where(t => t.UserId == userId).ToList();
    }
    
    // Create separate file for this sort of functions
    public static bool IsValidHexColor(string color)
    {
        string pattern = @"^#(?:[0-9a-fA-F]{3}){1,2}$";
        return Regex.IsMatch(color, pattern);
    }
    public Task Post(Task task)
    {
        
        bool taskExists = _context.tasks.Any(t => t.Name == task.Name);
        
        if(taskExists)
        {
            throw new InvalidOperationException("Task already exists");
        }

        if (!IsValidHexColor(task.Color))
        {
            throw new InvalidOperationException("Invalid color");
        }
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

    public List<WorkSession> GetWorkSessionsPastYear(string userId)
    {
        DateTime endDate = DateTime.Today;
        DateTime startDate = endDate.AddYears(-1);

        List<WorkSession> workSessions = _context.worksessions
            .Where(e => e.End.HasValue
                        && e.End.Value.Date >= startDate.Date
                        && e.End.Value.Date <= endDate.Date
                        && e.Task.UserId == userId)
            .Include(e => e.Task)
            .ToList();

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
                
                return newWorkSessions;
        
    }

    
}