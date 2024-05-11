using Microsoft.AspNetCore.Mvc;
using TaskProjectBackend.Application.Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WebSocketController : ControllerBase
{
    private readonly WebSocketService _webSocketService;

    public WebSocketController(WebSocketService webSocketService)
    {
        _webSocketService = webSocketService;
    }

    [HttpPost("start")]
    public IActionResult StartWebSocketServer()
    {
        _webSocketService.StartWebSocketServer("ws://192.168.100.8:8080");
        return Ok();
    }
    
    [HttpPost("stop")]
    public IActionResult StopWebSocketServer()
    {
        _webSocketService.StopWebSocketServer();
        return Ok();
    }
}