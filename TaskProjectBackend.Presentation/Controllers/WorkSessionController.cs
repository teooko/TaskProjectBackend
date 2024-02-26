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
    public ActionResult<string> StartWorkingSession(int id)
    {
        _workSessionService.Start(id);
        return Ok("Work session started");
    }

    [HttpPatch("{id}")]
    public ActionResult<string> StopWorkingSession(int id)
    {
        _workSessionService.Stop(id);
        return Ok("Work session stopped");
    }
}