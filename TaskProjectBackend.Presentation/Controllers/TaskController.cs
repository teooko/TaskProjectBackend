using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskProjectBackend.Application.DTO;
using TaskProjectBackend.Application.Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;
    public TaskController(TaskService taskService)
    {
        _taskService = taskService;
    }
    [HttpPost, Authorize]
    public ActionResult<Domain.Task> Post([FromBody] Domain.Task task)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        task.UserId = userId;
        return Ok(_taskService.Post(task));
    }
    [HttpGet("{id}")]
    public ActionResult<Domain.Task> Get(int id)
    {
        return Ok(_taskService.Get(id));
    }
    
    [HttpGet, Authorize]
    public ActionResult<List<Domain.Task>> Get()
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok(_taskService.Get(userId));
    }
    
    [HttpGet("date/{date}")]
    public ActionResult<List<TaskDTO>> GetTasksByDay(DateTime date)
    {
        return Ok(_taskService.GetTasksByDay(date));
    }
    [HttpDelete]
    public ActionResult<Domain.Task> Delete(int id)
    {
        return Ok(_taskService.Delete(id));
    }

    [HttpGet("{id}/duration")]
    public ActionResult<TimeSpan> GetDuration(int id)
    {
        return Ok(_taskService.GetDuration(id));
    }
    [HttpGet("weekly/{fromDate}"), Authorize]
    public ActionResult<List<WeeklyTasksDTO>> GetWeeklyTasks(int fromDate)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok(_taskService.GetWeeklyTasks(userId, fromDate));
    }
    [HttpGet("monthly")]
    public ActionResult<List<MonthlyTasksDTO>> GetMonthlyTasks()
    {
        string userId = HttpContext.Request.Headers["userId"];
        return Ok(_taskService.GetMontlyTasks(userId));
    }
    
    [HttpGet("total")]
    public ActionResult<List<MonthlyTasksDTO>> GetTotalTasksTime()
    {
        string userId = HttpContext.Request.Headers["userId"];
        return Ok(_taskService.GetTotalTasksTime(userId));
    }
}
