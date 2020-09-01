using System.Collections.Generic;

namespace Bookmark.Models
{
  public class Bookmark
  {
    public IEnumerable<Article> Articles { get; private set;  }

    public Bookmark(IEnumerable<Article> articles)
    {
        Articles = articles;
    }
  }
}
