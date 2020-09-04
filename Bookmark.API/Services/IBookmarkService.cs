using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookmark.Services
{
    public interface IBookmarkService
    {
        Task<string> Add(Models.Bookmark bookmark);
        Task<IEnumerable<Models.Bookmark>> GetAllBookmarks();
        Task<Models.Bookmark> GetById(string id);
    }
}
