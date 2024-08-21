using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SinalRServerExample.Hubs;

namespace SinalRServerExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        readonly IHubContext<MyHub> _hubContext;

        public HomeController(IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string message)
        {
            await _hubContext.Clients.All.SendAsync("receiveMessage",message);
            return Ok();

        }
    }
}
