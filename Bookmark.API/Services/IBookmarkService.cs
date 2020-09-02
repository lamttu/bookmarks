using System.Collections.Generic;

namespace Bookmark.Services
{
    public interface IBookmarkService
    {
        string Add(Models.Bookmark bookmark);
        IEnumerable<Models.Bookmark> GetAllBookmarks();
    }
}
