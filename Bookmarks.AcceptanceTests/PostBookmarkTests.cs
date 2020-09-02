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
            var bookmark = new Models.Bookmark
            {
                Articles = new List<Article>(),
                Id = "1",
                Name = "bookmark1"
            };
            var request = new HttpRequestMessage(HttpMethod.Post, "/bookmarks")
            {
                Content = new StringContent(JsonSerializer.Serialize(bookmark), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(request);

            Assert.True(response.IsSuccessStatusCode, $"Actual status code: {response.StatusCode}");
        }
    }
}

