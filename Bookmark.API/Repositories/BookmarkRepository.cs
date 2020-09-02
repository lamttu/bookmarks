using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Bookmark.Repositories
{
    public class BookmarkRepository : IBookmarkRepository
    {
        public async Task<IEnumerable<Models.Bookmark>> GetAllBookmarks()
        {
            var bookmarks = new List<Models.Bookmark>();

            await using var conn =
                new SqlConnection("Data Source=XLW-5CG8508H05;Initial Catalog=Bookmarks;User ID=webapp;Password=P@ssw0rd!;TrustServerCertificate=True;Database=Bookmarks");
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
