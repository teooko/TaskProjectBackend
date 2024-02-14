using Microsoft.AspNetCore.Mvc;
using TaskProjectBackend.Application.Services;

namespace TaskProjectBackend.Presentation.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TaskController : Controller
{
    
    private readonly TaskService _taskService = new TaskService();
    
    [HttpPost]
    public ActionResult<Domain.Task> Post([FromBody] Domain.Task task)
    {
        /*
        DGenre postedGenre = _genreService.Post(genre);
        
        if(postedGenre == null)
            return this.BadRequest("Name required");
        */  
        Console.WriteLine(task);
        return this.Ok(/*_taskService.Post(task)*/);
    }
    /*
    [HttpGet]
    public ActionResult<Domain.Task> Get()
    {
        // return this.Ok(_taskService.Get());
    }
    */
}