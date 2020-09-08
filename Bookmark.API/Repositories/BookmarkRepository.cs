using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Bookmark.Repositories
{
    public class BookmarkRepository : IBookmarkRepository
    {
        public readonly IConfiguration Configuration;
        private readonly ILogger<IBookmarkRepository> _logger;
        private readonly IDbConnection _db;

        public BookmarkRepository(IConfiguration configuration, ILogger<IBookmarkRepository> logger)
        {
            Configuration = configuration;
            _logger = logger;
            this._db = new NpgsqlConnection(Configuration["ConnectionStrings:BookmarksDatabase"]);
        }

        public async Task<IEnumerable<Models.Bookmark>> GetAllBookmarks()
        {
            return await _db.QueryAsync<Models.Bookmark>("SELECT id, bookmarkName AS name FROM bookmarks");
        }

        public async Task<Models.Bookmark> GetById(string id)
        {
            return await _db.QueryFirstOrDefaultAsync<Models.Bookmark>("SELECT id, bookmarkName AS name FROM bookmarks WHERE id = @Id", new {id});
        }

        public async Task<string> Add(Models.Bookmark bookmark)
        {
            try
            {
                await _db.ExecuteAsync("INSERT INTO bookmarks (id, bookmarkName) VALUES (@Id, @Name)", bookmark);
            }
            catch (NpgsqlException exception)
            {
                _logger.LogError(exception, "Error while inserting a new bookmark");
                throw;
            }
            return bookmark.Id;
        }

        public async Task<int> Delete(string id)
        {
            return await _db.ExecuteAsync("DELETE FROM bookmarks WHERE id = @Id", new {id});
        }
    }
}
