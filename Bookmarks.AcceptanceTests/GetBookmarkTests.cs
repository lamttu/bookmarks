using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;
using Bookmark.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Bookmark.AcceptanceTests
{
    public class GetBookmarkTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public GetBookmarkTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_Returns200_WhenReceivesARequest()
        {
            var response = await _client.GetAsync("/bookmarks");

            Assert.True(response.IsSuccessStatusCode, $"Actual status code: {response.StatusCode}");
        }

        [Fact]
        public async Task Get_ShouldReturnThePostedBookmark_AfterPosting()
        {
            var id = Guid.NewGuid().ToString();
            var bookmark = new Models.Bookmark
            {
                Articles = new List<Article>(),
                Id = id,
                Name = "test-bookmark"
            };
            var request = new HttpRequestMessage(HttpMethod.Post, "/bookmarks")
            {
                Content = new StringContent(JsonSerializer.Serialize(bookmark), Encoding.UTF8, "application/json")
            };

            await _client.SendAsync(request);
            var response = await _client.GetAsync($"/bookmarks/{id}");

            Assert.True(response.IsSuccessStatusCode, $"Actual status code: {response.StatusCode}");
        }

        [Fact]
        public async Task Get_ShouldReturn404_WhenRetrieveNonExistentFile()
        {
            var response = await _client.GetAsync($"/bookmarks/nonexistent-bookmark");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
