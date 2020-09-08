using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookmark.Repositories
{
    public interface IBookmarkRepository
    {
        Task<IEnumerable<Models.Bookmark>> GetAllBookmarks();
        Task<Models.Bookmark> GetById(string id);
        Task<string> Add(Models.Bookmark bookmark);
        Task<int> Delete(string bookmarkId);
    }
}
