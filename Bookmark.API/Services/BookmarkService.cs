using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookmark.Services
{
    public class BookmarkService : IBookmarkService
    {
        public List<Models.Bookmark> Bookmarks = new List<Models.Bookmark>();

        public string Add(Models.Bookmark bookmark)
        {
            return bookmark.Id;
        }

        public IEnumerable<Models.Bookmark> GetAllBookmarks()
        {
            return Bookmarks;
        }
    }
}
