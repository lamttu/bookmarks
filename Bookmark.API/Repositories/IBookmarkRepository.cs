using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookmark.Repositories
{
    public interface IBookmarkRepository
    {
        Task<IEnumerable<Models.Bookmark>> GetAllBookmarks();
    }
}
