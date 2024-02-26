using Domain;
using Microsoft.AspNetCore.Mvc;
using TaskProjectBackend.Application.Services;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkSessionController : ControllerBase
{
    private readonly WorkSessionService _workSessionService = new WorkSessionService();
    
    [HttpGet("{id}")]
    public ActionResult<WorkSession> Get(int id)
    {
        return Ok(_workSessionService.Get(id));
    }
    
    [HttpPost("{id}")]
    public ActionResult<string> Post(int id)
    {
        return Ok("Work session started");
    }
}