using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
       
        [Authorize]
        [HttpGet("names")]
        public IActionResult GetNames()
        {
            
            var names = new List<string>
            {
                "Pera",
                "Mika"
            };

            return Ok(names);
        }
    }
}
