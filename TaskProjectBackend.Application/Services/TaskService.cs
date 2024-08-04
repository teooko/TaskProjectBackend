using Domain;
using TaskProjectBackend.Application.DTO;
using TaskProjectBackend.DataAccess.Repositories;

namespace TaskProjectBackend.Application.Services;

public class TaskService
{
    private readonly TaskRepository _taskRepository;
    private readonly WorkSessionRepository _workSessionRepository;
    public TaskService(TaskRepository taskRepository, WorkSessionRepository workSessionRepository)
    {
        _taskRepository = taskRepository;
        _workSessionRepository = workSessionRepository;
    }
    public List<Domain.Task> Get(string userId)
    {
        var tasks = _taskRepository.Get(userId);
        return tasks;
    }
    public Domain.Task Get(int id)
    {
        var task = _taskRepository.Get(id);
        return task;
    }
    public Domain.Task Post(Domain.Task task)
    {
        return _taskRepository.Post(task);
    }
    public Domain.Task Delete(int id)
    {
        return _taskRepository.Delete(id);
    }

    public TimeSpan? GetDuration(int id)
    {
        List<WorkSession> workSessions = _taskRepository.GetWorkSessions(id);

        TimeSpan? duration = TimeSpan.Zero;
        foreach (var workSession in workSessions)
        {
            duration += workSession.End - workSession.Start;
        }

        return duration;
    }

    public List<TaskDTO> GetTasksByDay(string userId, DateTime date)
    {
        List<TaskDTO> tasks = new List<TaskDTO>();
        List<WorkSession> workSessions = _workSessionRepository.GetWorkSessionsByDay(userId, date);
    
        foreach (var workSession in workSessions)
        {
            var existingTask = tasks.FirstOrDefault(t => t.Id == workSession.Task.Id);
            if (existingTask != null)
            {
                existingTask.Time += workSession.End - workSession.Start;
            }
            else
            {
                TaskDTO task = new TaskDTO();
                task.Id = workSession.Task.Id;
                task.Color = workSession.Task.Color;
                task.Name = workSession.Task.Name;
                task.Time = workSession.End - workSession.Start;
                tasks.Add(task);
            }
        }
        return tasks;
    }

    public List<WeeklyTasksDTO> GetWeeklyTasks(string userId, int fromDate)
    {
        
        List<WorkSession> workSessions = _taskRepository.GetWeeklyWorkSessions(userId, fromDate);
        List<WeeklyTasksDTO> weeklyTasks = new List<WeeklyTasksDTO>();
        WeeklyTasksDTO weeklyTask = new WeeklyTasksDTO();
        if (workSessions.Count == 0)
            return new List<WeeklyTasksDTO>(0);
        
        weeklyTask.Day = workSessions[0].End.Value.Date;

        foreach (var workSession in workSessions)
        {
            if (workSession.End.Value.Date != weeklyTask.Day)
            {   weeklyTasks.Add(weeklyTask);
                weeklyTask = new WeeklyTasksDTO();
                weeklyTask.Day = workSession.End.Value.Date;
                weeklyTask.Colors.Add(workSession.Task.Color);
            }
            else
            {
                string existingColor = weeklyTask.Colors.FirstOrDefault(t => t == workSession.Task.Color);
                if (existingColor == null)
                {
                    weeklyTask.Colors.Add(workSession.Task.Color);
                }
            }
        }
        if(weeklyTask != null)
            weeklyTasks.Add(weeklyTask);
        
        return weeklyTasks;
    }
    
    public Dictionary<DateTime, HashSet<string>> GetPastYearTasks(string userId)
    {
        List<WorkSession> workSessions = _taskRepository.GetWorkSessionsPastYear(userId);

        var dailyTasks = new Dictionary<DateTime, HashSet<string>>();

        if (!workSessions.Any())
            return dailyTasks;

        foreach (var workSession in workSessions)
        {
            DateTime sessionDate = workSession.End.Value.Date;
            string color = workSession.Task.Color;

            if (!dailyTasks.ContainsKey(sessionDate))
            {
                dailyTasks[sessionDate] = new HashSet<string>();
            }

            dailyTasks[sessionDate].Add(color);
        }

        return dailyTasks;
    }
    
    public List<MonthlyTasksDTO> GetMontlyTasks(string userId)
    {
        List<WorkSession> workSessions = _taskRepository.GetMonthlyWorkSessions(userId, 6);
        List<MonthlyTasksDTO> monthlyTasksDtos = new List<MonthlyTasksDTO>();
        MonthlyTasksDTO monthlyTasksDto = new MonthlyTasksDTO();
        monthlyTasksDto.MonthNumber = workSessions[0].End.Value.Month;
        
        foreach (var workSession in workSessions)
        {
            if (workSession.End.Value.Month != monthlyTasksDto.MonthNumber)
            {
                monthlyTasksDtos.Add(monthlyTasksDto);
                monthlyTasksDto = new MonthlyTasksDTO();
                monthlyTasksDto.MonthNumber = workSession.End.Value.Month;
                
            }
            monthlyTasksDto.Time += (workSession.End - workSession.Start);
        }
        if (monthlyTasksDto != null)
            monthlyTasksDtos.Add(monthlyTasksDto);
        

        return monthlyTasksDtos;
    }
    
    public List<TotalTasksTimeDTO> GetTotalTasksTime(string userId)
    {
        return _workSessionRepository.GetAllWorkSessions()
            .Where(ws => ws.Task.UserId == userId)
            .GroupBy(ws => new { ws.Task.Id, ws.Task.Name })
            .Select(group => new TotalTasksTimeDTO
            {
                Id = group.Key.Id,
                Name = group.Key.Name,
                Time = TimeSpan.FromMinutes(group.Sum(ws => (ws.End.Value - ws.Start).TotalMinutes))
            })
            .ToList();
    }
}