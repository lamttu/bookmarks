using System.Collections.Generic;
using System.Threading.Tasks;
using Bookmark.Controllers;
using Bookmark.Models;
using Bookmark.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Bookmarks.UnitTests
{
    public class BookmarksControllerTests
    {
        private BookmarksController controller;
        private readonly Mock<IBookmarkService> _stubBookmarkService = new Mock<IBookmarkService>();
        private readonly Mock<ILogger<BookmarksController>> _logger = new Mock<ILogger<BookmarksController>>();

        public BookmarksControllerTests()
        {
            controller = new BookmarksController(_logger.Object, _stubBookmarkService.Object);
        }

        [Fact]
        public async Task Get_Returns404_WhenNoBookmarkIsFound()
        {
            var bookmarkId = "test";
            _stubBookmarkService.Setup(s => s.GetById(bookmarkId)).ReturnsAsync((Bookmark.Models.Bookmark) null);

            var response = await controller.GetById(bookmarkId);

            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task Get_Returns200WithTheBookmark_WhenNoBookmarkIsFound()
        {
            var bookmarkId = "test";
            var bookmark = new Bookmark.Models.Bookmark
            {
                Articles = new List<Article>(),
                Id = bookmarkId,
                Name = "test-bookmark"
            };
            _stubBookmarkService.Setup(s => s.GetById(bookmarkId)).ReturnsAsync(bookmark);

            var response = await controller.GetById(bookmarkId);
            var objectResult = response as OkObjectResult;
            var actual = objectResult.Value as Bookmark.Models.Bookmark;

            Assert.IsType<OkObjectResult>(response);
            actual.Should().BeEquivalentTo(bookmark);
        }
    }
}
