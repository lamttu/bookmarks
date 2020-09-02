using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookmark.Models;
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
        public IActionResult Get()
        {
            var bookmarks = _bookmarkService.GetAllBookmarks();
            return new OkObjectResult(bookmarks);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Bookmark bookmark)
        {
            _bookmarkService.Add(bookmark);
            return Created($"bookmarks/{bookmark.Id}", bookmark);
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("deleted an article from bookmark");
        }
    }
}
