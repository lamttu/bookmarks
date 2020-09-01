using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bookmark.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Bookmark.AcceptanceTests
{
    public class PostBookmarkTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public PostBookmarkTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_Returns201_WhenReceivesARequest()
        {
            var bookmark = new Models.Bookmark(new List<Article>(), "bookmark1");
            var json = JsonSerializer.Serialize(bookmark);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/bookmarks", content);

            Assert.True(response.IsSuccessStatusCode, $"Actual status code: {response.StatusCode}");
        }
    }
}

