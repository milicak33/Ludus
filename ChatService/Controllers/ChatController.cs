using Microsoft.AspNetCore.Mvc;
using ChatService.Entities;

namespace ChatService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    [HttpGet("test")]
    public ActionResult<Message> GetTestMessage()
    {
        return Ok(new Message
        {
            Sender = "System",
            Text = "Chat service is running",
            Timestamp = DateTime.UtcNow
        });
    }
}
