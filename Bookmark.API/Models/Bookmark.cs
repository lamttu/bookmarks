using System.Collections.Generic;

namespace Bookmark.Models
{
    public class Bookmark
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Article> Articles { get; set; }
    }
}
