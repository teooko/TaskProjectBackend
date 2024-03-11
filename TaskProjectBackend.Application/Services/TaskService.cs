﻿using Domain;
using TaskProjectBackend.Application.DTO;
using TaskProjectBackend.DataAccess.Repositories;

namespace TaskProjectBackend.Application.Services;

public class TaskService
{
    private readonly TaskRepository _taskRepository;
    private readonly WorkSessionRepository _workSessionRepository;
    public TaskService()
    {
        _taskRepository = new TaskRepository();
        _workSessionRepository = new WorkSessionRepository();
    }
    public List<Domain.Task> Get()
    {
        var tasks = _taskRepository.Get();
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

    public List<TaskDTO> GetTasksByDay(DateTime date)
    {
        List<TaskDTO> tasks = new List<TaskDTO>();
        List<WorkSession> workSessions = _workSessionRepository.GetWorkSessionsByDay(date);
        
        foreach (var workSession in workSessions)
        {
            
            if (!tasks.Any(t => t.Id == workSession.Task.Id))
            {
                TaskDTO task = new TaskDTO();
                task.Id = workSession.Task.Id;
                task.Color = workSession.Task.Color;
                task.Name = workSession.Task.Name;
                task.Time += workSession.End - workSession.Start;
                tasks.Add(task);
            }
            
        }
        return tasks;
    }
}