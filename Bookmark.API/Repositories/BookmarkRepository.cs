using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Bookmark.Repositories
{
    public class BookmarkRepository : IBookmarkRepository
    {
        public readonly IConfiguration Configuration;
        private IDbConnection db;

        public BookmarkRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            this.db = new SqlConnection(Configuration["ConnectionStrings:BookmarksDatabase"]);
        }

        public async Task<IEnumerable<Models.Bookmark>> GetAllBookmarks()
        {
            return await db.QueryAsync<Models.Bookmark>("SELECT id, name FROM Bookmark");
        }

        public async Task<Models.Bookmark> GetById(string id)
        {
            return await db.QueryFirstOrDefaultAsync<Models.Bookmark>("SELECT id, name FROM Bookmark WHERE id = @id", new {id});
        }
    }
}
