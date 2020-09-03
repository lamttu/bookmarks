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

            return bookmarks;
        }

        public async Task<Models.Bookmark> GetById(string id)
        {
            var bookmark = await _bookmarkRepository.GetById(id);

            return bookmark;
        }
    }
}
