using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookmark.Repositories;

namespace Bookmark.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository _bookmarkRepository;
        private readonly IArticleRepository _articleRepository;

        public BookmarkService(IBookmarkRepository bookmarkRepository, IArticleRepository articleRepository)
        {
            _bookmarkRepository = bookmarkRepository;
            _articleRepository = articleRepository;
        }

        public string Add(Models.Bookmark bookmark)
        {
            return bookmark.Id;
        }

        public async Task<IEnumerable<Models.Bookmark>> GetAllBookmarks()
        {
            var bookmarks = await _bookmarkRepository.GetAllBookmarks();
            var bookmarksWithArticles = bookmarks.Select(async b => new Models.Bookmark()
            {
                Id = b.Id,
                Name = b.Name,
                Articles = await _articleRepository.GetArticlesFromBookmark(b.Id)
            });

            var result = await Task.WhenAll(bookmarksWithArticles);

            return result;
        }
    }
}
