using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Bookmark.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Bookmark.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        public readonly IConfiguration Configuration;
        private readonly IDbConnection _db;

        public ArticleRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            this._db = new NpgsqlConnection(Configuration["ConnectionStrings:BookmarksDatabase"]);
        }

        public async Task<IEnumerable<Article>> GetArticlesFromBookmark(string bookmarkId)
        {
            return await _db.QueryAsync<Article>("SELECT id, name, website FROM Article WHERE bookmarkId = @bookmarkId",
                new {bookmarkId});
        }
    }
}
