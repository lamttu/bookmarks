using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Bookmark.Repositories
{
    public class BookmarkRepository : IBookmarkRepository
    {
        public readonly IConfiguration Configuration;
        private readonly IDbConnection _db;

        public BookmarkRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            this._db = new NpgsqlConnection(Configuration["ConnectionStrings:BookmarksDatabase"]);
        }

        public async Task<IEnumerable<Models.Bookmark>> GetAllBookmarks()
        {
            return await _db.QueryAsync<Models.Bookmark>("SELECT id, bookmarkName AS name FROM bookmarks");
        }

        public async Task<Models.Bookmark> GetById(string id)
        {
            return await _db.QueryFirstOrDefaultAsync<Models.Bookmark>("SELECT id, bookmarkName AS name FROM bookmarks WHERE id = @id", new {id});
        }
    }
}
