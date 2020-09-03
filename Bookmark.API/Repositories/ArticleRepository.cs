using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Bookmark.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Bookmark.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        public readonly IConfiguration Configuration;
        private IDbConnection db;

        public ArticleRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            this.db = new SqlConnection(Configuration["ConnectionStrings:BookmarksDatabase"]);
        }

        public async Task<IEnumerable<Article>> GetArticlesFromBookmark(string bookmarkId)
        {
            return await db.QueryAsync<Article>("SELECT id, name, website FROM Article WHERE bookmarkId = @bookmarkId",
                new {bookmarkId});
        }
    }
}
