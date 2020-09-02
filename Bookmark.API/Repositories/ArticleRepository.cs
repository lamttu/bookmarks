using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Bookmark.Models;

namespace Bookmark.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        public async Task<IEnumerable<Article>> GetArticlesFromBookmark(string bookmarkId)
        {
            var articles = new List<Article>();
            await using var conn =
                new SqlConnection("Data Source=XLW-5CG8508H05;Initial Catalog=Bookmarks;User ID=webapp;Password=P@ssw0rd!;TrustServerCertificate=True;Database=Bookmarks");
            {
                var command = new SqlCommand($"SELECT name, website FROM Article WHERE bookmarkId='{bookmarkId}'", conn);
                await command.Connection.OpenAsync();
                await using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var article = new Article()
                    {
                        BookmarkId = bookmarkId,
                        Name = reader.GetString(0),
                        Website = reader.GetString(1)
                    };
                    articles.Add(article);
                }
            }

            return articles;
        }
    }
}
