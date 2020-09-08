using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bookmark.Repositories;
using Bookmark.Services;
using Moq;
using Xunit;

namespace Bookmarks.UnitTests
{
    public class BookmarkServiceTests
    {
        private readonly Mock<IBookmarkRepository> _bookmarkRepository = new Mock<IBookmarkRepository>();
        private readonly Mock<IArticleRepository> _articleRepository = new Mock<IArticleRepository>();
        private readonly BookmarkService _bookmarkService;

        public BookmarkServiceTests()
        {
            _bookmarkService = new BookmarkService(_bookmarkRepository.Object, _articleRepository.Object);        
        }

        [Fact]
        public async Task Delete_ShouldReturnRowsDeleted_AfterDeletingThem()
        {
            var bookmarkId = "test-bookmark";
            _bookmarkRepository.Setup(b => b.Delete(bookmarkId)).ReturnsAsync(1);

            var row = await _bookmarkService.Delete(bookmarkId);

            Assert.Equal(1, row);
        }

        [Fact]
        public async Task Delete_ShouldDeleteArticles_WhenDeletingBookmark()
        {
            var bookmarkId = "test-bookmark";

            var row = await _bookmarkService.Delete(bookmarkId);

            _articleRepository.Verify(a => a.DeleteArticlesWithBookmarkId(bookmarkId), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldNotDeleteBookmark_WhenDeletingArticlesFails()
        {
            var bookmarkId = "test-bookmark";
            _articleRepository.Setup(a => a.DeleteArticlesWithBookmarkId(bookmarkId)).ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(() => _bookmarkService.Delete(bookmarkId));
            _bookmarkRepository.Verify(b => b.Delete(bookmarkId), Times.Never);
        }

    }
}
