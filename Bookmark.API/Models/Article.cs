namespace Bookmark.Models
{
  public class Article
  {
    public string Name { get; private set; }
    public string Website { get; private set; }

    public Article(string name, string website)
    {
        Name = name;
        Website = website;
    }
  }
}