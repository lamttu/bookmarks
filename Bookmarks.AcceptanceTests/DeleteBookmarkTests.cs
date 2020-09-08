using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bookmark.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Bookmark.AcceptanceTests
{
    public class DeleteBookmarkTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public DeleteBookmarkTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Delete_Returns200_WhenReceivesARequest()
        {
            var id = Guid.NewGuid().ToString(); 
            var bookmark = new Models.Bookmark
            {
                Articles = new List<Article>{ new Article() { Id = Guid.NewGuid().ToString(), BookmarkId = id, Name = "test-article", Website = "testarticle.com"}},
                Id = id,
                Name = "bookmark1"
            };
            var request = new HttpRequestMessage(HttpMethod.Post, "/bookmarks")
            {
                Content = new StringContent(JsonSerializer.Serialize(bookmark), Encoding.UTF8, "application/json")
            };

            await _client.SendAsync(request);

            var response = await _client.DeleteAsync($"/bookmarks/{id}");

            Assert.True(response.IsSuccessStatusCode, $"Actual status code: {response.StatusCode}");
        }
    }
}