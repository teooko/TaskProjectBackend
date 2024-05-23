using System.Net.WebSockets;
using System.Security.Claims;
using Domain;
using Fleck;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskProjectBackend.Application.Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupSessionController : ControllerBase
{
    private readonly GroupSessionService _groupSessionService;
    private readonly WebSocketService _webSocketService;
    public GroupSessionController(GroupSessionService groupSessionService, WebSocketService webSocketService)
    {
        _groupSessionService = groupSessionService;
        _webSocketService = webSocketService;
    }
    
    [HttpPost, Authorize]
    public ActionResult<GroupSession> Post()
    {
        
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        GroupSession groupSession = _groupSessionService.Post(userId);
        return Ok(groupSession);
    }
    
    [HttpPatch("{groupSessionId}")]
    public ActionResult<GroupSession> Stop(int groupSessionId)
    {
        GroupSession groupSession = _groupSessionService.Patch(groupSessionId);
        return groupSession;
    }
    
    [HttpPatch("{groupSessionId}/Join"), Authorize]
    public ActionResult<GroupSession> Join(int groupSessionId)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        GroupSession groupSession = _groupSessionService.Join(groupSessionId, userId);
        return groupSession;
    }
    
    [HttpPatch("{groupSessionId}/Leave"), Authorize]
    public ActionResult<GroupSession> Leave(int groupSessionId)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        GroupSession groupSession = _groupSessionService.Leave(groupSessionId, userId);
        return groupSession;
    }
}