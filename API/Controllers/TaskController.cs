using Microsoft.AspNetCore.Mvc;
using TaskProjectBackend.Application.Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService = new TaskService();
    
    [HttpPost]
    public ActionResult<Domain.Task> Post([FromBody] Domain.Task task)
    {
       
        return this.Ok(_taskService.Post(task));
    }

    [HttpGet("{id}")]
    public ActionResult<Domain.Task> Get(int id)
    {
        return this.Ok(_taskService.Get(id));
    }
}
