using System.Net.Http;
using System.Threading.Tasks;
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
    }
}
