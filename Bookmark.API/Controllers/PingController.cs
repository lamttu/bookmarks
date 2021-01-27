using System.Threading.Tasks;
using Bookmark.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookmark.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        private readonly ILogger<PingController> _logger;
        private readonly IBookmarkService _bookmarkService;

        public PingController(ILogger<PingController> logger, IBookmarkService bookmarkService)
        {
            _logger = logger;
            _bookmarkService = bookmarkService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult("OK");
        }        
    }
}
