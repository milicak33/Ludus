using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MatchmakingService.Application;

namespace MatchmakingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchmakingController : ControllerBase
{
    private readonly JoinQueueHandler _joinQueueHandler = new();

    [HttpPost("join")]
    public IActionResult Join([FromBody] JoinRequest payload)  
    {
        var result = _joinQueueHandler.Handle(payload.PlayerId, payload.Rating);
        return Ok(new { message = result });
    }

    [HttpGet("status/{playerId}")]
    public IActionResult Status(string playerId)
    {
        return Ok(new { status = "searching" });
    }
}
