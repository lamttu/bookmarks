using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Bookmark.Repositories
{
    public class BookmarkRepository : IBookmarkRepository
    {
        public readonly IConfiguration Configuration;

        public BookmarkRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<IEnumerable<Models.Bookmark>> GetAllBookmarks()
        {
            var bookmarks = new List<Models.Bookmark>();

            await using var conn =
                new SqlConnection(Configuration["ConnectionStrings:BookmarksDatabase"]);
            {
                var command = new SqlCommand("SELECT id, name FROM Bookmark", conn);
                await command.Connection.OpenAsync();
                await using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var bookmark = new Models.Bookmark()
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1)
                    };

                    bookmarks.Add(bookmark);
                }
            }
            
            return bookmarks;
        }
    }
}
