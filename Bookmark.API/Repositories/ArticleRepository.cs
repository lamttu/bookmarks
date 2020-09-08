using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Bookmark.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Bookmark.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        public readonly IConfiguration Configuration;
        private readonly ILogger<IBookmarkRepository> _logger;
        private readonly IDbConnection _db;

        public ArticleRepository(IConfiguration configuration, ILogger<IBookmarkRepository> logger)
        {
            Configuration = configuration;
            _logger = logger;
            this._db = new NpgsqlConnection(Configuration["ConnectionStrings:BookmarksDatabase"]);
        }

        public async Task<string> Add(Article article)
        {
            try
            {
                await _db.ExecuteAsync("INSERT INTO articles (id, articleName, website, bookmarkId) VALUES (@Id, @Name, @Website, @BookmarkId)", article);
            }
            catch (NpgsqlException exception)
            {
                _logger.LogError(exception, "Error while inserting a new article");
                throw;
            }
            return article.Id;

        }

        public async Task<int> DeleteArticlesWithBookmarkId(string bookmarkId)
        {
            return await _db.ExecuteAsync("DELETE FROM articles WHERE bookmarkId = @bookmarkId", new {bookmarkId});
        }

        public async Task<IEnumerable<Article>> GetArticlesFromBookmark(string bookmarkId)
        {
            return await _db.QueryAsync<Article>("SELECT id, articleName as name, website, bookmarkId FROM articles WHERE bookmarkId = @bookmarkId",
                new {bookmarkId});
        }
    }
}
