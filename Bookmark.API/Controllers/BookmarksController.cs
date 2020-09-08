using System.Threading.Tasks;
using Bookmark.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookmark.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookmarksController : ControllerBase
    {
        private readonly ILogger<BookmarksController> _logger;
        private readonly IBookmarkService _bookmarkService;

        public BookmarksController(ILogger<BookmarksController> logger, IBookmarkService bookmarkService)
        {
            _logger = logger;
            _bookmarkService = bookmarkService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var bookmarks = await _bookmarkService.GetAllBookmarks();
            return new OkObjectResult(bookmarks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var bookmark = await _bookmarkService.GetById(id);
            if(bookmark == null) {
                return new NotFoundResult();
            }
            return new OkObjectResult(bookmark);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Bookmark bookmark)
        {
            await _bookmarkService.Add(bookmark);
            return Created($"bookmarks/{bookmark.Id}", bookmark);
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("deleted an article from bookmark");
        }
    }
}
