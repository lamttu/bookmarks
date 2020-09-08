using System.Collections.Generic;
using System.Threading.Tasks;
using Bookmark.Models;

namespace Bookmark.Repositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticlesFromBookmark(string bookmarkId);
        Task<string> Add(Article article);
        Task<int> DeleteArticlesWithBookmarkId(string bookmarkId);
    }
}
