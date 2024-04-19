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
    [HttpPost]
    public ActionResult<Domain.Task> Post([FromBody] Domain.Task task)
    {
        return Ok(_taskService.Post(task));
    }
    [HttpGet("{id}")]
    public ActionResult<Domain.Task> Get(int id)
    {
        return Ok(_taskService.Get(id));
    }
    
    [HttpGet]
    public ActionResult<List<Domain.Task>> Get()
    {
        return Ok(_taskService.Get());
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
    [HttpGet("weekly/{fromDate}")]
    public ActionResult<List<WeeklyTasksDTO>> GetWeeklyTasks(int fromDate)
    {
        return Ok(_taskService.GetWeeklyTasks(fromDate));
    }
    [HttpGet("monthly")]
    public ActionResult<List<MonthlyTasksDTO>> GetMonthlyTasks()
    {
        return Ok(_taskService.GetMontlyTasks());
    }
    
    [HttpGet("total")]
    public ActionResult<List<MonthlyTasksDTO>> GetTotalTasksTime()
    {
        return Ok(_taskService.GetTotalTasksTime());
    }
}
