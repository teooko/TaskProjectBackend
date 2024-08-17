using System.Security.Claims;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskProjectBackend.Application.DTO;
using TaskProjectBackend.Application.Services;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkSessionController : ControllerBase
{
    private readonly WorkSessionService _workSessionService;

    public WorkSessionController(WorkSessionService workSessionService)
    {
        _workSessionService = workSessionService;
    }
    
    [HttpGet("{id}")]
    public ActionResult<WorkSession> GetDuration(int id)
    {
        return Ok(_workSessionService.GetDuration(id));
    }
    
    [HttpPost("{id}")]
    public ActionResult<WorkSession> StartWorkingSession(int id)
    {
        WorkSession workSession = _workSessionService.Start(id);
        return Ok(workSession);
    }

    [HttpPatch("{id}")]
    public ActionResult<string> StopWorkingSession(int id)
    {
        _workSessionService.Stop(id);
        return Ok("Work session stopped");
    }

    [HttpPost, Authorize]
    public ActionResult<WorkSession> PostWorkSession(WorkSessionDTO workSessionDto)
    {
        return _workSessionService.PostWorkSession(workSessionDto);
    }
}