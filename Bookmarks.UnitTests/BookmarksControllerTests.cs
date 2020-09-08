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
        private readonly BookmarksController _controller;
        private readonly Mock<IBookmarkService> _stubBookmarkService = new Mock<IBookmarkService>();
        private readonly Mock<ILogger<BookmarksController>> _logger = new Mock<ILogger<BookmarksController>>();

        public BookmarksControllerTests()
        {
            _controller = new BookmarksController(_logger.Object, _stubBookmarkService.Object);
        }

        [Fact]
        public async Task Get_Returns404_WhenNoBookmarkIsFound()
        {
            var bookmarkId = "test";
            _stubBookmarkService.Setup(s => s.GetById(bookmarkId)).ReturnsAsync((Bookmark.Models.Bookmark) null);

            var response = await _controller.GetById(bookmarkId);

            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task Get_Returns200WithTheBookmark_WhenNoBookmarkIsFound()
        {
            var bookmarkId = "test-id";
            var bookmark = new Bookmark.Models.Bookmark
            {
                Articles = new List<Article>(),
                Id = bookmarkId,
                Name = "test-bookmark"
            };
            _stubBookmarkService.Setup(s => s.GetById(bookmarkId)).ReturnsAsync(bookmark);

            var response = await _controller.GetById(bookmarkId);
            var objectResult = response as OkObjectResult;
            var actual = objectResult.Value as Bookmark.Models.Bookmark;

            Assert.IsType<OkObjectResult>(response);
            actual.Should().BeEquivalentTo(bookmark);
        }

        [Fact]
        public async Task Delete_Returns400_WhenCalledWithNoBookmarkId()
        {
            var response = await _controller.Delete(string.Empty);

            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task Delete_Returns404_WhenCalledWithNonExistentBookmarkId()
        {
            var bookmarkId = "test-id";
            _stubBookmarkService.Setup(s => s.GetById(bookmarkId)).ReturnsAsync((Bookmark.Models.Bookmark) null);
            var response = await _controller.Delete(bookmarkId);

            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task Delete_Returns200_WhenCalledWithExistentBookmarkId()
        {
            var bookmarkId = "test-id";
            var bookmark = new Bookmark.Models.Bookmark
            {
                Articles = new List<Article>(),
                Id = bookmarkId,
                Name = "test-bookmark"
            };
            _stubBookmarkService.Setup(s => s.GetById(bookmarkId)).ReturnsAsync(bookmark);

            var response = await _controller.Delete(bookmarkId);

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async Task Delete_DeletesBookmark_WhenCalledWithExistentBookmarkId()
        {
            var bookmarkId = "test-id";
            var bookmark = new Bookmark.Models.Bookmark
            {
                Articles = new List<Article>(),
                Id = bookmarkId,
                Name = "test-bookmark"
            };
            _stubBookmarkService.Setup(s => s.GetById(bookmarkId)).ReturnsAsync(bookmark);

            var response = await _controller.Delete(bookmarkId);

            _stubBookmarkService.Verify(s => s.Delete(bookmarkId), Times.Once);
        }
    }
}
