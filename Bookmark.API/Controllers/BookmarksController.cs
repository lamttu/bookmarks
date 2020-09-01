using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookmark.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookmarksController : ControllerBase
    {
        private readonly ILogger<BookmarksController> _logger;

        public BookmarksController(ILogger<BookmarksController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("hey");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("added a new article in bookmark");
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("deleted an article from bookmark");
        }
    }
}
