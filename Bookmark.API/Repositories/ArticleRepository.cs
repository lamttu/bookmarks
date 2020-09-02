using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Bookmark.Models;
using Microsoft.Extensions.Configuration;

namespace Bookmark.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        public readonly IConfiguration Configuration;

        public ArticleRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<IEnumerable<Article>> GetArticlesFromBookmark(string bookmarkId)
        {
            var articles = new List<Article>();
            await using var conn =
                new SqlConnection(Configuration["ConnectionStrings:BookmarksDatabase"]);
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
