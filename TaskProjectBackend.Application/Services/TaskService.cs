﻿using TaskProjectBackend.DataAccess.Repositories;

namespace TaskProjectBackend.Application.Services;

public class TaskService
{
    private readonly TaskRepository _taskRepository;

    public TaskService()
    {
        _taskRepository = new TaskRepository();
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
    
}