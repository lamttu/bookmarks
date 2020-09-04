using System.Collections.Generic;
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

        public async Task<string> Add(Models.Bookmark bookmark)
        {
            await _bookmarkRepository.Add(bookmark);

            foreach (var article in bookmark.Articles)
            {
               await _articleRepository.Add(article);
            }

            return bookmark.Id;
        }

        public async Task<IEnumerable<Models.Bookmark>> GetAllBookmarks()
        {
            return await _bookmarkRepository.GetAllBookmarks();
        }

        public async Task<Models.Bookmark> GetById(string id)
        {
            var bookmark = await _bookmarkRepository.GetById(id);
            bookmark.Articles = await _articleRepository.GetArticlesFromBookmark(id);

            return bookmark;
        }
    }
}
